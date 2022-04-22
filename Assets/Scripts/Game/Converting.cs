using UnityEngine;

public class Converting : MonoBehaviour 
{
    [SerializeField] CellGameObjects cellGameObjects;
    [SerializeField] CellSymbol cellSymbol;
    public static (int, int) GetDirection(string directionString)
    {
        int i = 0, j = 0;
        if (directionString == "W")
            j = 1;
        else if (directionString == "S")
            j = -1;
        else if (directionString == "D")
            i = 1;
        else if (directionString == "A")
            i = -1;
        return (i, j);
    }

    public static (int, int) GetNewPostion((int i, int j) OldPosition, (int i, int j) direction) =>
        (OldPosition.i + direction.i, OldPosition.j + direction.j);

    public GameObject[,] GetGameObjectsField(char [,] CharField)
    {
        GameObject[,] GameObjectsField = new GameObject[CharField.GetLength(0), CharField.GetLength(1)];
        for (int i = 0; i < CharField.GetLength(0); i++)
            for (int j = 0; j < CharField.GetLength(1); j++)
                GameObjectsField[i, j] = GetGameObjectCell(CharField[i, j]);

        return GameObjectsField;
    }
    private GameObject GetGameObjectCell(char CharCell)
    {
        GameObject GameObjectCell = null;
        switch (CharCell)
        {
            case CellSymbol.PlayerSymbol:
                GameObjectCell = cellGameObjects.PlayerGameObject;
                break;
            case CellSymbol.KeySymbol:
                GameObjectCell = cellGameObjects.KeyGameObject;
                break;
            case CellSymbol.WallSymbol:
                GameObjectCell = cellGameObjects.WallGameObject;
                break;
            case CellSymbol.ExitSymbol:
                GameObjectCell = cellGameObjects.ExitGameObject;
                break;
            case CellSymbol.EmptySymbol:
                GameObjectCell = null;
                break;
            default:
                break;
        }
        return GameObjectCell;
    }

    public bool GetCondition(Vector3 Direction, Vector3 NewPosition, Vector3 OldPosition)
    {
        if (Direction.x == 1)
            return NewPosition.x <= OldPosition.x;
        else if (Direction.x == -1)
            return NewPosition.x >= OldPosition.x;
        else if (Direction.z == 1)
            return NewPosition.z <= OldPosition.z;
        else if (Direction.z == -1)
            return NewPosition.z >= OldPosition.z;
        return false;
    }

    public int GetAnimationIndex((int x, int y) Direction)
    {
        if (Direction.y == 1)
            return 1;
        else if (Direction.y == -1)
            return 2;
        else if (Direction.x == 1)
            return 3;
        if (Direction.x == -1)
            return 4;
        else
            return 0;
    }
}
