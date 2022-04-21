using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject LobbyGameObject;
    [SerializeField] private Game game;
    void Awake()
    {
        game.StartNewRound();
    }
}