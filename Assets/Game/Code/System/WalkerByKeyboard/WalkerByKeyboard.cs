using UnityEngine;

public class WalkerByKeyboard : MonoBehaviour
{
    [Header("By GetComponent")]
    public Stats stats;
    public KeyboardInput input;

    void Start()
    {
        stats = GetComponent<Stats>();
        input = GetComponent<KeyboardInput>();
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