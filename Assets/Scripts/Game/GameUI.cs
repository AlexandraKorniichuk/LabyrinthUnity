using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject KeyImage;
    [SerializeField] Text MovesAmount;
    public void TurnOnKeyImage() => KeyImage.SetActive(true);
    public void UpdateMovesAmount(int MovesLeftAmount) => MovesAmount.text = $"{MovesLeftAmount}";
}
