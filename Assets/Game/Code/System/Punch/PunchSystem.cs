using System.Collections.Generic;
using UnityEngine;

// #jam
public class PunchSystem : MonoBehaviour
{
    public static List<Punch> entities = new List<Punch>();

    private void FixedUpdate()
    {
        foreach (var e in entities)
        {
            if (e.force.magnitude > 0)
            {
                e.tt("Punch")
                    .Add(() =>
                    {
                        if (e.randomDir)
                            e.stats.direction = (Random.insideUnitCircle * 10).normalized;
                    })
                    .Loop(0.13f, t => e.rbody.velocity = Vector2.zero)
                    .Loop(0.2f, t =>
                    {
                        e.rbody.velocity += e.force;
                        e.force = Vector2.Lerp(e.force, Vector2.zero, Easef.EaseIn(t.t));
                    })
                    .Add(t =>
                    {
                        e.force = Vector2.zero;
                        t.self.Reset();
                    })
                    .Immutable();
            }
        }
    }
}