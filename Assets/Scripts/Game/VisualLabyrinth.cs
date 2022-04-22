using UnityEngine;
using System.Collections;

public class VisualLabyrinth : MonoBehaviour
{
    [SerializeField] Converting converting;

    private GameObject Player;
    private GameObject Key;
    private Animator PlayerAnimation;

    public bool IsEndMovePlayer = true;
    private float speedMovingPlayer = 3f;

    private void FindSpecialObjects()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerAnimation = Player.GetComponentInChildren<Animator>();
        Key = GameObject.FindGameObjectWithTag("Key");
    }

    public void DrawLabyrinth(char[,] CharField)
    {
        GameObject[,] LabyrinthObejects = converting.GetGameObjectsField(CharField);
        CraeteObjects(LabyrinthObejects);
        FindSpecialObjects();
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
        StartCoroutine(MovePlayer(NewPlayerPosition));
        IsEndMovePlayer = false;
        if (HaveGotKey)
            Key.SetActive(false);
    }

    private IEnumerator MovePlayer((int x, int z) Direction)
    {
        SetAnimation(Direction);
        Vector3 VectorDirection = GetVectorDirection(Direction);
        Vector3 NewGameObjectPosition = Player.transform.position + VectorDirection;

        while (converting.GetCondition(VectorDirection, Player.transform.position, NewGameObjectPosition))
        {
            Player.transform.Translate(VectorDirection * Time.deltaTime * speedMovingPlayer);
            yield return new WaitForEndOfFrame();
        }
        Player.transform.position = RoundUpPosition();

        Direction = (0, 0);
        SetAnimation(Direction);

        IsEndMovePlayer = true;
    }

    private Vector3 GetVectorDirection((int x, int z) Direction) =>
        new Vector3(Direction.x, 0f, Direction.z);

    private Vector3 RoundUpPosition() {
        float x = Mathf.Round(Player.transform.position.x);
        float y = Player.transform.position.y;
        float z = Mathf.Round(Player.transform.position.z);
        return new Vector3 (x, y, z); 
    }

    private void SetAnimation((int x, int z) Direction)
    {
        int index = converting.GetAnimationIndex(Direction);
        PlayerAnimation.SetInteger("Direction", index);
    }

    public void ShowExitIsWrong()
    {

    }
}
