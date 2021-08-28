using System.Collections.Generic;
using UnityEngine;

// #jam
public class DeadSystem : MonoBehaviour
{
    public static List<Dead> entities = new List<Dead>();

    private void Update()
    {
        foreach (var e in entities)
        {
            if (e.deads.Count > 0)
            {
                var stats = e.deads[0];
                e.deads.RemoveAt(0);

                if (e.stats.health > 0)
                    continue;

                var pos = e.transform.position;
                e.tt("Dead")
                    .Reset()
                    .Add(() =>
                    {
                        ExplosionSystem.Get().At(pos, 8);
                    })
                    .Add(0.5f, () =>
                    {
                        pos = e.transform.position;
                        ExplosionSystem.Get().At(pos, 8);
                        e.transform.position += Vector3.one * 9999;
                    })
                    .Add(0.2f, () =>
                    {
                        DeadTankSystem.Get().At(pos);
                    });
            }
        }
    }
}