using UnityEngine;

public class RotateByKeyboard : MonoBehaviour
{
    [Header("Required")]
    public Transform target;

    [Header("By GetComponent")]
    public KeyboardInput input;

    void Start()
    {
        input = GetComponent<KeyboardInput>();
    }

    void OnEnable()
    {
        RotateByKeyboardSystem.rotates.Add(this);
    }

    void OnDisable()
    {
        RotateByKeyboardSystem.rotates.Remove(this);
    }
}