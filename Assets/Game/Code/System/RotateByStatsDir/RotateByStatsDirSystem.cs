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

            // Diagonals

            if (c.lastDir.x > 0.5f && c.lastDir.y > 0.5f)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 45);

            else if (c.lastDir.x < -0.5f && c.lastDir.y > 0.5f)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 135);

            else if (c.lastDir.x > 0.5f && c.lastDir.y < -0.5f)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 315);

            else if (c.lastDir.x < -0.5f && c.lastDir.y < -0.5f)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 225);

            // Rects

            else if (c.lastDir.x > 0.5f)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 0);

            else if (c.lastDir.x < -0.5f)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 180);

            else if (c.lastDir.y > 0.5f)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 90);

            else if (c.lastDir.y < -0.5f)
                c.target.transform.localEulerAngles = new Vector3(0, 0, 270);
        }
    }
}