using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public Vector3 wasd;

    void OnEnable()
    {
        KeyboardInputSystem.keys.Add(this);
    }

    void OnDisable()
    {
        KeyboardInputSystem.keys.Remove(this);
    }
}