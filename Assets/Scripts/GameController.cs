using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject LobbyGameObject;
    [SerializeField] private GameObject Greeting;
    [SerializeField] private Button PlayButton;
    [SerializeField] private Game game;
    [SerializeField] EndGame endGame;
    void Awake()
    {
        if (PlayButton != null)
            PlayButton.onClick.AddListener(game.StartNewRound);
    }

    public void EndRound()
    {
        LobbyGameObject.SetActive(true);
        Greeting.SetActive(false);
        endGame.OpenEndRoundPanel();
    }
}