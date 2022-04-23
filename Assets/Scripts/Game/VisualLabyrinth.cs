using UnityEngine;
using System.Collections;

public class VisualLabyrinth : MonoBehaviour
{
    [SerializeField] Converting converting;

    private GameObject Player;
    private GameObject Key;
    private GameObject[] Exits;
    private Animator PlayerAnimation;

    public bool IsEndMovePlayer = true;
    private float speedMovingPlayer = 3f;

    private void FindSpecialObjects()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerAnimation = Player.GetComponentInChildren<Animator>();
        Key = GameObject.FindGameObjectWithTag("Key");
        Exits = GameObject.FindGameObjectsWithTag("Exit");
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
        Vector3 VectorDirection = GetVectorFromTurple(Direction);
        Vector3 NewGameObjectPosition = Player.transform.position + VectorDirection;

        while (converting.GetCondition(VectorDirection, Player.transform.position, NewGameObjectPosition))
        {
            Player.transform.Translate(VectorDirection * Time.deltaTime * speedMovingPlayer);
            yield return new WaitForEndOfFrame();
        }
        Player.transform.position = RoundUpPosition(Player.transform.position, IsRoundY: false);

        Direction = (0, 0);
        SetAnimation(Direction);

        IsEndMovePlayer = true;
    }

    private Vector3 GetVectorFromTurple((int x, int z) Turple) =>
        new Vector3(Turple.x, 0f, Turple.z);

    private Vector3 RoundUpPosition(Vector3 Position, bool IsRoundY = true) {
        float x = Mathf.Round(Position.x);
        float y;
        if (IsRoundY) y = Mathf.Round(Position.y);
        else y = Position.y;

        float z = Mathf.Round(Position.z);
        return new Vector3 (x, y, z); 
    }

    private void SetAnimation((int x, int z) Direction)
    {
        int index = converting.GetAnimationIndex(Direction);
        PlayerAnimation.SetInteger("Direction", index);
    }

    public void ShowExitIsWrong((int, int) ExitPosition)
    {
        Vector3 VectorExitPosition = GetVectorFromTurple(ExitPosition);
        foreach (GameObject Exit in Exits)
        {
            if (VectorExitPosition == RoundUpPosition(Exit.transform.position))
                Exit.GetComponent<WrongExit>().ShowExitIsWrong();
        }
    }
}
