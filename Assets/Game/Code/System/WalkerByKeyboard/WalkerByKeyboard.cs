using UnityEngine;

public class WalkerByKeyboard : MonoBehaviour
{
    public KeyboardInput keyboardInput;
    public Stats stats;

    void Start()
    {
        keyboardInput = GetComponent<KeyboardInput>();
        stats = GetComponent<Stats>();
    }

    void OnEnable()
    {
        WalkerByKeyboardSystem.walkers.Add(this);
    }

    void OnDisable()
    {
        WalkerByKeyboardSystem.walkers.Remove(this);
    }
}