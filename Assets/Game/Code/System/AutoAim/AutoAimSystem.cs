using System.Collections.Generic;
using UnityEngine;

// #jam
public class AutoAimSystem : MonoBehaviour
{
    public static List<AutoAim> aims = new List<AutoAim>();
    public static List<AutoAimTarget> targets = new List<AutoAimTarget>();

    private void Update()
    {
        AutoAimTarget closest = null;
        float closestDistance = 9999;

        foreach (var a in aims)
        {
            if (a.stats.direction == Vector3.zero)
                continue;

            var apos = a.transform.position;
            foreach (var t in targets)
            {
                var distance = (apos - t.transform.position).sqrMagnitude;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = t;
                }
            }

            if (closest == null)
                continue;

            var dir = (closest.transform.position - a.transform.position);
            a.stats.direction = Vector3.Lerp(a.stats.direction, dir, Time.deltaTime * 0.1f);
        }

    }
}