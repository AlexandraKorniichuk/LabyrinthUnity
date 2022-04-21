using UnityEngine;

public class DrawingLabyrinth : MonoBehaviour
{
    [SerializeField] Converting converting;

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

    public void UpdateLabyrinth()
    {

    }
}
