using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject KeyImage;
    [SerializeField] Text O2Amount;
    public void TurnOnKeyImage() => KeyImage.SetActive(true);
    public void UpdateMovesAmount(int MovesLeftAmount) => O2Amount.text = $"{MovesLeftAmount}";
}
