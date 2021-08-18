using System.Collections.Generic;
using UnityEngine;

public class RotateByKeyboardSystem : MonoBehaviour
{
    public static List<RotateByKeyboard> rotates = new List<RotateByKeyboard>();

    void Update()
    {
        foreach (var r in rotates)
        {
            if (r.input.wasd.x == 1)
                r.target.transform.localEulerAngles = new Vector3(0, 0, 0);

            if (r.input.wasd.x == -1)
                r.target.transform.localEulerAngles = new Vector3(0, 180, 0);

            if (r.input.wasd.y == 1)
                r.target.transform.localEulerAngles = new Vector3(0, 0, 90);

            if (r.input.wasd.y == -1)
                r.target.transform.localEulerAngles = new Vector3(0, 180, 270);
        }
    }
}