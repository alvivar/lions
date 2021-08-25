using UnityEngine;

// #jam
public class SpeedByInput : MonoBehaviour
{
    public int keyDown = 0;

    [Header("By GetComponent")]
    public KeyboardInput input;
    public Stats stats;

    private void Start()
    {
        input = GetComponent<KeyboardInput>();
        stats = GetComponent<Stats>();
    }

    private void OnEnable()
    {
        SpeedByInputSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        SpeedByInputSystem.entities.Remove(this);
    }
}