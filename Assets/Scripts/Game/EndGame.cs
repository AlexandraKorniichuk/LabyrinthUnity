using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] string Wintext;
    [SerializeField] string Losetext;
    [SerializeField] Text EndGameText;
    [SerializeField] AnimatedObjectState animatedObjectState;

    public void OpenEndRoundPanel()
    {
        gameObject.SetActive(true);
        animatedObjectState.StartAnimation();
        SetEndGameText();
    }

    private void SetEndGameText()
    {
        if (Game.IsWin)
            EndGameText.text = Wintext;
        else
            EndGameText.text = Losetext;
    }
}
