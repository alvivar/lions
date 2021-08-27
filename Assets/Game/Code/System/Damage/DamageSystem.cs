using System.Collections.Generic;
using UnityEngine;

// #jam
public class DamageSystem : MonoBehaviour
{
    public static List<Damage> components = new List<Damage>();

    private void Update()
    {
        foreach (var c in components)
        {
            c.delay -= Time.deltaTime;

            if (c.collisions.Count > 0)
            {
                var stats = c.collisions[0];
                c.collisions.RemoveAt(0);

                if (c.delay > 0)
                    continue;

                c.delay = 0.10f;
                c.stats.health -= stats.damage;

                if (c.OnDamage != null)
                    c.OnDamage(stats);
            }
        }
    }
}