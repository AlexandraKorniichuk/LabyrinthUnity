using UnityEngine;

public class InputController
{
    public static KeyCode GetInputMovementKey()
    {
        KeyCode inputKey;
        do
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                inputKey = KeyCode.W;
                break;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                inputKey = KeyCode.S;
                break;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                inputKey = KeyCode.A;
                break;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                inputKey = KeyCode.D;
                break;
            }
        } while (true);
        return inputKey;
    }
}
