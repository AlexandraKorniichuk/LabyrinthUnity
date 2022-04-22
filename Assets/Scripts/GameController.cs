using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject LobbyGameObject;
    [SerializeField] private Button PlayButton;
    [SerializeField] private Game game;
    void Awake()
    {
        if (PlayButton != null)
            PlayButton.onClick.AddListener(game.StartNewRound);
        
    }
}