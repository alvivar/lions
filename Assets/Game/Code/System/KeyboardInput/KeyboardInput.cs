using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public Vector3 wasd;
    public Vector3 lastWasd;
    public int spaceDown = 0;

    void OnEnable()
    {
        KeyboardInputSystem.keys.Add(this);
    }

    void OnDisable()
    {
        KeyboardInputSystem.keys.Remove(this);
    }
}