using System;

namespace Labyrinth
{
    public class Game
    {
        private (int i, int j) FieldSize = (10, 15);
        private const int WallChance = 25;

        private const int MaxMovesAmount = 40;
        private int MovesAmountLeft = MaxMovesAmount;

        private (int, int) PlayerPosition;
        private (int, int) KeyPosition;
        private (int, int)[] ExitPositions;
        private const int ExitsAmount = 3;

        private bool HaveGotKey = false;

        public static bool IsWin;

        private Random rand = new Random();
        public void StartNewRound()
        {
            IsWin = false;

            CreateSpecialObjectsPositions();
            char[,] GameField = CreateField();
            (int, int) RightExit = ChooseRightExit();

            do
            {
                DrawField(GameField);
                WriteMessages(RightExit);

                CheckHavingKey();

                (int, int) direction = InputDirection();
                (int, int) NewPosition = Converting.GetNewPostion(PlayerPosition, direction);

                if (TryMove(GameField, NewPosition))
                    Move(NewPosition);

                MovesAmountLeft--;
                Console.Clear();
            } while (!IsEndGame(RightExit));
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
                    int randNumber = rand.Next(0, 100);
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

        private (int, int) GetRandomPosition() =>
            (rand.Next(0, FieldSize.i), rand.Next(0, FieldSize.j));

        private (int, int) ChooseRightExit() =>
            ExitPositions[rand.Next(0, ExitsAmount)];

        private void DrawField(char[,] Field)
        {
            for (int i = 0; i < FieldSize.i; i++)
            {
                for (int j = 0; j < FieldSize.j; j++)
                {
                    char cell;
                    if (PlayerPosition == (i, j))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        cell = CellSymbol.PlayerSymbol;
                    }
                    else if (KeyPosition == (i, j) && !HaveGotKey)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        cell = CellSymbol.KeySymbol;
                    }
                    else if (IsCellExit((i, j)))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        cell = CellSymbol.ExitSymbol;
                    }
                    else
                    {
                        cell = Field[i, j];
                    }
                    Console.Write(cell);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }

        private (int, int) InputDirection()
        {
            ConsoleKey inputKeyMovement = InputController.GetInputMovementKey();
            return Converting.GetDirection(inputKeyMovement.ToString());
        }

        private bool TryMove(char[,] Field, (int, int) NewPosition)
        {
            if (IsNewPositionOutOfField(NewPosition))
                return false;
            else if (IsNewPositionWall(Field, NewPosition))
                return false;

            return true;
        }

        private bool IsNewPositionWall(char[,] Field, (int i, int j) NewPosition) =>
            Field[NewPosition.i, NewPosition.j] == CellSymbol.ExitSymbol;

        private bool IsNewPositionOutOfField((int i, int j) NewPosition) =>
            NewPosition.i < 0 || NewPosition.j < 0 || NewPosition.i >= FieldSize.i || NewPosition.j >= FieldSize.j;

        private void Move((int, int) NewPosition) =>
            PlayerPosition = NewPosition;

        private void WriteMessages((int, int) RightExit)
        {
            WriteMovesMessage();
            WriteKeyMessage();
            WriteExitMessage(RightExit);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void WriteMovesMessage() =>
            Console.WriteLine($"Moves left: {MovesAmountLeft}");

        private void WriteKeyMessage()
        {
            if (HavePlayerReachedKey())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You've got a key!");
            }
        }

        private void WriteExitMessage((int, int) RightExit)
        {
            if (IsCellExit(PlayerPosition) || (PlayerPosition == RightExit && !HaveGotKey))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Closed!");
            }
        }

        private void CheckHavingKey()
        {
            if (HavePlayerReachedKey())
                HaveGotKey = true;
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
}