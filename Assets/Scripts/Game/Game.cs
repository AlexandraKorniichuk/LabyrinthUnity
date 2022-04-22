using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    private (int i, int j) FieldSize = (15, 15);
    private char[,] GameField;
    private const int WallChance = 25;

    private const int MaxMovesAmount = 55;
    private int MovesAmountLeft = MaxMovesAmount;

    private (int i, int j) PlayerPosition;
    private (int i, int j) KeyPosition;
    private (int i, int j)[] ExitPositions;
    private const int ExitsAmount = 3;
    private (int, int) RightExit;

    private (int, int) NewPosition;

    private bool HaveGotKey = false;

    public static bool IsWin;

    [SerializeField] GameObjectsInLabyrinth labyrinth;
    public void StartNewRound()
    {
        IsWin = false;

        CreateSpecialObjectsPositions();
        GameField = CreateField();
        GameField = GetFieldWithSpecialObjects(GameField);
        DrawField(GameField);

        NewPosition = PlayerPosition;
        RightExit = ChooseRightExit();

        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        do
        {
            if (!labyrinth.IsEndMovePlayer)
                yield return new WaitWhile(() => labyrinth.IsEndMovePlayer);

            yield return new WaitWhile(() => InputController.GetInputMovementKey() == KeyCode.None);
            //WriteMessages(RightExit);

            (int, int) direction = InputDirection(InputController.InputKey);

            NewPosition = Converting.GetNewPostion(PlayerPosition, direction);

            if (TryMove(GameField, NewPosition))
            {
                Move(NewPosition);
                CheckHavingKey();
                UpdateField(direction);
            }
            print(MovesAmountLeft);
            MovesAmountLeft--;
            yield return new WaitForEndOfFrame();
        } while (!IsEndGame(RightExit));
        print($"Is win: {IsWin}");
    }

    private void CreateSpecialObjectsPositions()
    {
        PlayerPosition = GetRandomPosition();
        KeyPosition = GetRandomPosition();
        ExitPositions = new (int, int)[ExitsAmount];
        for (int i = 0; i < ExitsAmount; i++)
            ExitPositions[i] = GetRandomPosition();
    }

    private char[,] CreateField()
    {
        char[,] Field = new char[FieldSize.i, FieldSize.j];
        for (int i = 0; i < FieldSize.i; i++)
        {
            for (int j = 0; j < FieldSize.j; j++)
            {
                int randNumber = UnityEngine.Random.Range(0, 100);
                char cell;
                if (IsCellSpecialObject((i, j)))
                    cell = CellSymbol.EmptySymbol;
                else if (WallChance > randNumber)
                    cell = CellSymbol.WallSymbol;
                else
                    cell = CellSymbol.EmptySymbol;

                Field[i, j] = cell;
            }
        }
        return Field;
    }

    private bool IsCellSpecialObject((int, int) currentCell) =>
        PlayerPosition == currentCell || KeyPosition == currentCell || IsCellExit(currentCell);

    private bool IsCellExit((int, int) currentCell)
    {
        foreach ((int, int) ExitPosition in ExitPositions)
        {
            if (currentCell == ExitPosition)
                return true;
        }
        return false;
    }

    private char[,] GetFieldWithSpecialObjects(char [,] Field)
    {
        Field[PlayerPosition.i, PlayerPosition.j] = CellSymbol.PlayerSymbol;
        Field[KeyPosition.i, KeyPosition.j] = CellSymbol.KeySymbol;

        for (int i = 0; i < ExitsAmount; i++)
            Field[ExitPositions[i].i, ExitPositions[i].j] = CellSymbol.ExitSymbol;

        return Field;
    }

    private (int, int) GetRandomPosition() =>
        (UnityEngine.Random.Range(0, FieldSize.i), UnityEngine.Random.Range(0, FieldSize.j));

    private (int, int) ChooseRightExit() =>
        ExitPositions[UnityEngine.Random.Range(0, ExitsAmount)];

    private void DrawField(char[,] Field) => labyrinth.DrawLabyrinth(Field);

    private void UpdateField((int, int) Direction) => labyrinth.UpdateLabyrinth(Direction, HaveGotKey);

    private (int, int) InputDirection(KeyCode inputKeyMovement) =>
        Converting.GetDirection(inputKeyMovement.ToString());

    private bool TryMove(char[,] Field, (int, int) NewPosition)
    {
        if (IsNewPositionOutOfField(NewPosition))
            return false;
        else if (IsNewPositionWall(Field, NewPosition))
            return false;

        return true;
    }

    private bool IsNewPositionWall(char[,] Field, (int i, int j) NewPosition) =>
        Field[NewPosition.i, NewPosition.j] == CellSymbol.WallSymbol;

    private bool IsNewPositionOutOfField((int i, int j) NewPosition) =>
        NewPosition.i < 0 || NewPosition.j < 0 || NewPosition.i >= FieldSize.i || NewPosition.j >= FieldSize.j;

    private void Move((int, int) NewPosition) =>
        PlayerPosition = NewPosition;

    //private void WriteMessages((int, int) RightExit)
    //{
    //    WriteMovesMessage();
    //    WriteKeyMessage();
    //    WriteExitMessage(RightExit);
    //    Console.ForegroundColor = ConsoleColor.White;
    //}

    //private void WriteMovesMessage() =>
    //    Console.WriteLine($"Moves left: {MovesAmountLeft}");

    //private void WriteKeyMessage()
    //{
    //    if (HavePlayerReachedKey())
    //    {
    //        Console.ForegroundColor = ConsoleColor.Yellow;
    //        Console.WriteLine("You've got a key!");
    //    }
    //}

    //private void WriteExitMessage((int, int) RightExit)
    //{
    //    if (IsCellExit(PlayerPosition) || (PlayerPosition == RightExit && !HaveGotKey))
    //    {
    //        Console.ForegroundColor = ConsoleColor.Red;
    //        Console.WriteLine("Closed!");
    //    }
    //}

    private void CheckHavingKey()
    {
        if (HavePlayerReachedKey())
        {
            HaveGotKey = true;
        }
    }

    private bool HavePlayerReachedKey() =>
        PlayerPosition == KeyPosition && !HaveGotKey;

    private bool IsEndGame((int, int) RightExit)
    {
        if (HaveGotKey && PlayerPosition == RightExit)
        {
            IsWin = true;
            return true;
        }
        else if (MovesAmountLeft == 0)
        {
            IsWin = false;
            return true;
        }
        return false;
    }
}
