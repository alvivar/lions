using System.Collections.Generic;
using UnityEngine;

// #jam
public class ExplosionOnDamageSystem : MonoBehaviour
{
    public static List<ExplosionOnDamage> entities = new List<ExplosionOnDamage>();

    private void Update()
    {
        foreach (var e in entities)
        {
            if (e.explosions.Count > 0)
            {
                var stats = e.explosions[0];
                e.explosions.RemoveAt(0);

                e.tt("Explosion")
                    .Reset()
                    .Loop(0.8f, t =>
                    {
                        e.render.material.color = Color.Lerp(e.render.material.color, Color.clear, Easef.EaseIn(t.t));
                        e.transform.localScale = Vector3.Lerp(e.transform.localScale, Vector3.one * 3, Easef.EaseIn(t.t));
                    })
                    .Add(() =>
                    {
                        e.render.material.color = Color.white;
                        e.transform.localScale = Vector3.one;
                    });
            }
        }
    }
}