using System.Collections.Generic;
using UnityEngine;

// #jam
public class HideOnDamageSystem : MonoBehaviour
{
    public static List<HideOnDamage> components = new List<HideOnDamage>();

    private void Update()
    {
        foreach (var c in components)
        {
            if (c.damages.Count > 0)
            {
                c.damages.RemoveAt(0);

                c.stats.direction = Vector2.zero;
                c.bullet.trail.enabled = false;
                c.tt().Add(0.1f, () => { c.bullet.RandomPosition(); });
            }
        }
    }
}