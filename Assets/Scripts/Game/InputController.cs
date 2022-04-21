using UnityEngine;

public class InputController : MonoBehaviour 
{
    public static KeyCode InputKey = KeyCode.None;
    public static KeyCode GetInputMovementKey()
    {
        InputKey = KeyCode.None;
        if (Input.GetKeyDown(KeyCode.W))
        {
            InputKey = KeyCode.W;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            InputKey = KeyCode.S;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            InputKey = KeyCode.A;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            InputKey = KeyCode.D;
        }
        return InputKey;
    }
}
