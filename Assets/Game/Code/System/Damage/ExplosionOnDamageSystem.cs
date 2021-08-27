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

                c.tt("Explosion")
                    .Reset()
                    .Loop(0.8f, t =>
                    {
                        c.render.material.color = Color.Lerp(
                            c.render.material.color,
                            Color.clear,
                            Easef.EaseIn(t.t));

                        c.transform.localScale = Vector3.Lerp(
                            c.transform.localScale,
                            Vector3.one * 4,
                            Easef.EaseIn(t.t));
                    })
                    .Add(() =>
                    {
                        c.render.material.color = Color.white;
                        c.transform.localScale = Vector3.one;
                    });
            }
        }
    }
}