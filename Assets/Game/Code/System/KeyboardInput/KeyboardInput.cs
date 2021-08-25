using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public Vector3 wasd;
    public Vector3 lastWasd;
    public int spaceDown = 0;
    public int lshift = 0;

    void OnEnable()
    {
        KeyboardInputSystem.entities.Add(this);
    }

    void OnDisable()
    {
        KeyboardInputSystem.entities.Remove(this);
    }
}