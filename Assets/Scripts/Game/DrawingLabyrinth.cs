using UnityEngine;

public class DrawingLabyrinth : MonoBehaviour
{
    [SerializeField] Converting converting;

    private GameObject Player;
    private GameObject Key;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Key = GameObject.FindGameObjectWithTag("Key");
    }

    public void DrawLabyrinth(char[,] CharField)
    {
        GameObject[,] LabyrinthObejects = converting.GetGameObjectsField(CharField);
        CraeteObjects(LabyrinthObejects);
    }

    private void CraeteObjects(GameObject[,] labyrinthObejects)
    {
        for (int i = 0; i < labyrinthObejects.GetLength(0); i++)
        {
            for (int j = 0; j < labyrinthObejects.GetLength(1); j++)
            {
                if (labyrinthObejects[i, j] != null)
                {
                    Vector3 position = GetObjectPostion(labyrinthObejects[i, j], i, j);
                    Instantiate(labyrinthObejects[i, j], position, Quaternion.identity);
                }
            }
        }
    }

    private Vector3 GetObjectPostion(GameObject obj, int i, int j) =>
         new Vector3(i, obj.transform.position.y, j);

    public void UpdateLabyrinth((int, int) NewPlayerPosition, bool HaveGotKey)
    {
        MovePlayer(NewPlayerPosition);
        if (HaveGotKey)
        {
            Key.SetActive(false);
            print("You've got a key");
        }
    }

    private void MovePlayer((int x, int z) NewPosition)
    {
        //Player.transform.Translate(NewPosition.x, Player.transform.position.y, NewPosition.z, Space.World); 
        Player.transform.position = new Vector3(NewPosition.x, Player.transform.position.y, NewPosition.z);
    }
}
