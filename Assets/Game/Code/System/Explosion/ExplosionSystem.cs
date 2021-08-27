using System.Collections.Generic;
using UnityEngine;

// #jam
public class ExplosionSystem : MonoBehaviour
{
    public static List<Explosion> components = new List<Explosion>();
    public static int index = 0;

    private void Update()
    {
        foreach (var c in components)
        {
            if (c.explodeAt.Count > 0)
            {
                var pos = c.explodeAt[0];
                c.explodeAt.RemoveAt(0);

                c.tt("Explosion")
                    .Reset()
                    .Add(() =>
                    {
                        c.transform.position = pos + new Vector3(0, 0, 1);
                        c.transform.localScale = new Vector3(1, 1, 0.1f);
                        c.render.material.color = Color.white;
                    })
                    .Loop(0.8f, t =>
                    {
                        c.render.material.color = Color.Lerp(
                            c.render.material.color,
                            Color.clear,
                            Easef.EaseIn(t.t)
                        );

                        c.transform.localScale = Vector3.Lerp(
                            c.transform.localScale,
                            new Vector3(8, 8, 0.1f),
                            Easef.EaseIn(t.t)
                        );
                    })
                    .Add(() =>
                    {
                        c.transform.position += Vector3.one * 9999;
                    });
            }
        }
    }

    public static Explosion Get()
    {
        var explosion = components[index];
        index = ++index % components.Count;

        return explosion;
    }
}