using System.Collections.Generic;
using UnityEngine;

// #jam
public class CamSystem : MonoBehaviour
{
    public static List<Cam> cams = new List<Cam>();
    public static List<CamTarget> targets = new List<CamTarget>();

    private void Update()
    {
        foreach (var c in cams)
        {
            Vector3 median = Vector3.zero;
            foreach (var t in targets)
                median += t.transform.position;
            median /= targets.Count;

            median.x = Mathf.Clamp(median.x, c.xLimit.x, c.xLimit.y);
            median.y = Mathf.Clamp(median.y, c.yLimit.x, c.yLimit.y);
            median.z = c.transform.position.z;

            c.transform.position = Vector3.Lerp(
                c.transform.position,
                median,
                Time.deltaTime * 0.9f);
        }
    }
}