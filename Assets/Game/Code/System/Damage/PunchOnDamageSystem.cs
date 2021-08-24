using System.Collections.Generic;
using UnityEngine;

// #jam
public class PunchOnDamageSystem : MonoBehaviour
{
    public static List<PunchOnDamage> entities = new List<PunchOnDamage>();

    private void Update()
    {
        foreach (var e in entities)
        {
            if (e.punches.Count > 0)
            {
                var stats = e.punches[0];
                e.punches.RemoveAt(0);

                var punch = stats.punch;
                var dir = (e.transform.position - stats.transform.position).normalized;

                e.punch.force = punch * dir;
            }
        }
    }
}