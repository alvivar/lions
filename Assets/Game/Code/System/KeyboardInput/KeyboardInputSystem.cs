using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInputSystem : MonoBehaviour
{
    public static List<KeyboardInput> keys = new List<KeyboardInput>();

    void Update()
    {
        int w = Keyboard.current.wKey.isPressed ? 1 : 0;
        int s = Keyboard.current.sKey.isPressed ? -1 : 0;
        int a = Keyboard.current.aKey.isPressed ? -1 : 0;
        int d = Keyboard.current.dKey.isPressed ? 1 : 0;

        foreach (var k in keys)
            k.wasd = new Vector3(a + d, w + s, 0);
    }
}