using System.Collections.Generic;
using UnityEngine;

// #jam
public class DamageSystem : MonoBehaviour
{
    public static List<Damage> entities = new List<Damage>();

    private void Update()
    {
        foreach (var e in entities)
        {
            if (e.collisions.Count > 0)
            {
                var stats = e.collisions[0];
                e.collisions.RemoveAt(0);

                e.stats.health -= stats.damage;

                if (e.OnDamage != null)
                    e.OnDamage(stats);
            }
        }
    }
}