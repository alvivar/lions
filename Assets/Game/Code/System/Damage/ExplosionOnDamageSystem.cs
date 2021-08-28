using System.Collections.Generic;
using UnityEngine;

// #jam
public class ExplosionOnDamageSystem : MonoBehaviour
{
    public static List<ExplosionOnDamage> components = new List<ExplosionOnDamage>();

    private void Update()
    {
        foreach (var c in components)
        {
            if (c.explosions.Count > 0)
            {
                c.explosions.RemoveAt(0);

                ExplosionSystem.Get().At(c.transform.position, scale : 6);
            }
        }
    }
}