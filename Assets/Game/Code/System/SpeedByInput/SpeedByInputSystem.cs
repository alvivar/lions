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
                    .Add(() =>
                    {
                        e.stats.speedOverride = e.stats.speed * 2;
                    })
                    .Loop(1f, t =>
                    {
                        Debug.Log($"{e.stats.speedOverride} at {Time.time}");
                        e.stats.speedOverride = Mathf.Lerp(
                            e.stats.speedOverride,
                            0,
                            Easef.EaseIn(t.t));
                    })
                    .Add(t => t.self.Reset())
                    .Immutable();
            }
        }
    }
}