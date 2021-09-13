using System.Collections.Generic;
using UnityEngine;

// #jam
public class RotateByStatsDirSystem : MonoBehaviour
{
    public static List<RotateByStatsDir> components = new List<RotateByStatsDir>();

    void Update()
    {
        foreach (var c in components)
        {
            if (c.stats.direction != Vector3.zero)
                c.lastDir = c.stats.direction;

            // Rects

            if (c.lastDir.x == 1)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 0);

            if (c.lastDir.x == -1)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 180);

            if (c.lastDir.y == 1)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 90);

            if (c.lastDir.y == -1)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 270);

            // Diagonals

            if (c.lastDir.x == 1 && c.lastDir.y == 1)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 45);

            if (c.lastDir.x == -1 && c.lastDir.y == 1)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 135);

            if (c.lastDir.x == 1 && c.lastDir.y == -1)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 315);

            if (c.lastDir.x == -1 && c.lastDir.y == -1)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 225);
        }
    }
}