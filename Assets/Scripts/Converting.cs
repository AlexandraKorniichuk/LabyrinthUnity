using UnityEngine;

public class Converting : MonoBehaviour 
{
    [SerializeField] CellGameObjects cellGameObjects;
    [SerializeField] CellSymbol cellSymbol;
    public static (int, int) GetDirection(string directionString)
    {
        int i = 0, j = 0;
        if (directionString == "W")
            i = -1;
        else if (directionString == "S")
            i = 1;
        else if (directionString == "D")
            j = 1;
        else if (directionString == "A")
            j = -1;
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
}
