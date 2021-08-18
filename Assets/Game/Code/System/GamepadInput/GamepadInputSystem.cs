using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadInputSystem : MonoBehaviour
{
    public static List<GamepadInput> inputs = new List<GamepadInput>();

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return;

        foreach (var input in inputs)
        {
            // Dpad

            input.dPadX = gamepad.dpad.x.ReadValue();
            input.dPadY = gamepad.dpad.y.ReadValue();

            // Left Stick

            input.leftStickX = gamepad.leftStick.x.ReadValue();
            input.leftStickY = gamepad.leftStick.y.ReadValue();

            if (gamepad.leftStickButton.wasPressedThisFrame)
                input.leftStickPressed += 1;

            if (gamepad.leftStickButton.wasReleasedThisFrame)
                input.leftStickReleased += 1;

            // Right Stick

            input.rightStickX = gamepad.rightStick.x.ReadValue();
            input.rightStickY = gamepad.rightStick.y.ReadValue();

            if (gamepad.rightStickButton.wasPressedThisFrame)
                input.rightStickPressed += 1;

            if (gamepad.rightStickButton.wasReleasedThisFrame)
                input.rightStickReleased += 1;

            // Start

            if (gamepad.startButton.wasPressedThisFrame)
                input.startPressed += 1;

            if (gamepad.startButton.wasReleasedThisFrame)
                input.startReleased += 1;

            // Select

            if (gamepad.selectButton.wasPressedThisFrame)
                input.selectPressed += 1;

            if (gamepad.selectButton.wasReleasedThisFrame)
                input.selectReleased += 1;

            // A

            if (gamepad.aButton.wasPressedThisFrame)
                input.aPressed += 1;

            if (gamepad.aButton.wasReleasedThisFrame)
                input.aReleased += 1;

            // X

            if (gamepad.xButton.wasPressedThisFrame)
                input.xPressed += 1;

            if (gamepad.xButton.wasReleasedThisFrame)
                input.xReleased += 1;

            // B

            if (gamepad.bButton.wasPressedThisFrame)
                input.bPressed += 1;

            if (gamepad.bButton.wasReleasedThisFrame)
                input.bReleased += 1;

            // Y

            if (gamepad.yButton.wasPressedThisFrame)
                input.yPressed += 1;

            if (gamepad.yButton.wasReleasedThisFrame)
                input.yReleased += 1;

            // Left Shoulder

            if (gamepad.leftShoulder.wasPressedThisFrame)
                input.leftShoulderPressed += 1;

            if (gamepad.leftShoulder.wasReleasedThisFrame)
                input.leftShoulderReleased += 1;

            // Right Shoulder

            if (gamepad.rightShoulder.wasPressedThisFrame)
                input.rightShoulderPressed += 1;

            if (gamepad.rightShoulder.wasReleasedThisFrame)
                input.rightShoulderReleased += 1;

            // Left Trigger

            if (gamepad.leftTrigger.wasPressedThisFrame)
                input.leftTriggerPressed += 1;

            if (gamepad.leftTrigger.wasReleasedThisFrame)
                input.leftTriggerReleased += 1;

            // Right Trigger

            if (gamepad.rightTrigger.wasPressedThisFrame)
                input.rightTriggerPressed += 1;

            if (gamepad.rightTrigger.wasReleasedThisFrame)
                input.rightTriggerReleased += 1;
        }
    }
}