using UnityEngine.UI;
using UnityEngine;

public class ExitButton : MonoBehaviour 
{
    [SerializeField] private GameObject Message;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(TryQuit);
    }
    public void TryQuit()
    {
        if (Game.IsWin)
            Quit();
        else
            ShowMessage();
    }

    private void ShowMessage() => Message.SetActive(true);

    private void Quit() => Application.Quit();
}
