using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// #jam
public class KeyboardInputSystem : MonoBehaviour
{
    public static List<KeyboardInput> entities = new List<KeyboardInput>();

    void Update()
    {
        int w = Keyboard.current.wKey.isPressed ? 1 : 0;
        int s = Keyboard.current.sKey.isPressed ? -1 : 0;
        int a = Keyboard.current.aKey.isPressed ? -1 : 0;
        int d = Keyboard.current.dKey.isPressed ? 1 : 0;

        bool lshift = Keyboard.current.leftShiftKey.wasPressedThisFrame;
        bool space = Keyboard.current.spaceKey.wasPressedThisFrame;

        foreach (var e in entities)
        {
            e.wasd = new Vector3(a + d, w + s, 0);

            if (e.wasd != Vector3.zero)
                e.lastWasd = e.wasd;

            if (space)
                e.spaceDown += 1;

            if (lshift)
                e.lshift += 1;
        }
    }
}