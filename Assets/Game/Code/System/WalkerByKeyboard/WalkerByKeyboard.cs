using UnityEngine;

public class WalkerByKeyboard : MonoBehaviour
{
    public KeyboardInput input;
    public Stats stats;

    void Start()
    {
        input = GetComponent<KeyboardInput>();
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