using UnityEngine;

// #jam
public class GamepadInput : MonoBehaviour
{
    [Header("Dpad")]
    public float dPadX = 0;
    public float dPadY = 0;

    [Header("Sticks")]
    public float leftStickX = 0;
    public float leftStickY = 0;
    public float rightStickX = 0;
    public float rightStickY = 0;

    [Header("Sticks Buttons")]
    public int leftStickPressed = 0;
    public int leftStickReleased = 0;
    public int rightStickPressed = 0;
    public int rightStickReleased = 0;

    [Header("Options")]
    public int startPressed = 0;
    public int startReleased = 0;
    public int selectPressed = 0;
    public int selectReleased = 0;

    [Header("A")]
    public int aPressed = 0;
    public int aReleased = 0;

    [Header("X")]
    public int xPressed = 0;
    public int xReleased = 0;

    [Header("B")]
    public int bPressed = 0;
    public int bReleased = 0;

    [Header("Y")]
    public int yPressed = 0;
    public int yReleased = 0;

    [Header("Shoulder")]
    public int leftShoulderPressed = 0;
    public int leftShoulderReleased = 0;
    public int rightShoulderPressed = 0;
    public int rightShoulderReleased = 0;

    [Header("Trigger")]
    public int leftTriggerPressed = 0;
    public int leftTriggerReleased = 0;
    public int rightTriggerPressed = 0;
    public int rightTriggerReleased = 0;

    void OnEnable()
    {
        GamepadInputSystem.inputs.Add(this);
    }

    void OnDisable()
    {
        GamepadInputSystem.inputs.Remove(this);
    }
}