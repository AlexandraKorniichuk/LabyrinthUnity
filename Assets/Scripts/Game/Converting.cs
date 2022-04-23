using UnityEngine;
using Labyrinth;

public class Converting : MonoBehaviour 
{
    [SerializeField] CellGameObjects cellGameObjects;
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
                GameObjectCell = GetRandomWallObject();
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

    private GameObject GetRandomWallObject()
    {
        int index = Random.Range(0, cellGameObjects.WallGameObjects.Count);
        return cellGameObjects.WallGameObjects[index];
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
