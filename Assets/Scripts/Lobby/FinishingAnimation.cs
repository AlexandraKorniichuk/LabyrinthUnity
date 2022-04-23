using UnityEngine;

public class FinishingAnimation : MonoBehaviour
{
    [SerializeField] GameObject anotherObject1;
    [SerializeField] GameObject anotherObject2;
    public void TurnOffObject() => gameObject.SetActive(false);
    public void TurnOffAnotherObjects()
    {
        anotherObject1.SetActive(false);
        anotherObject2.SetActive(false);
    }
    public void TurnOnAnotherObjects()
    {
        anotherObject1.SetActive(true);
        anotherObject2.SetActive(true);
    }
}
