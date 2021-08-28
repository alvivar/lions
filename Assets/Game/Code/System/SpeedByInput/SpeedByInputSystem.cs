using System.Collections.Generic;
using UnityEngine;

// #jam
public class SpeedByInputSystem : MonoBehaviour
{
    public static List<SpeedByInput> entities = new List<SpeedByInput>();

    private void Update()
    {
        foreach (var e in entities)
        {
            if (e.keyDown < e.input.lshift)
            {
                e.keyDown += 1;

                e.tt("Dash")
                    .Add(() => { e.stats.speedOverride = e.stats.speed * 8; })
                    .Loop(0.8f, t =>
                    {
                        e.stats.speedOverride = Mathf.Lerp(
                            e.stats.speedOverride,
                            e.stats.speed,
                            Easef.EaseIn(t.t));
                    })
                    .Add(t => { t.self.Reset(); })
                    .Immutable();
            }
        }
    }
}