using System.Collections.Generic;
using UnityEngine;

// #jam
public class HumanAnimSystem : MonoBehaviour
{
    public static List<HumanAnim> components = new List<HumanAnim>();

    private const string IDLE = "player.idle";
    private const string WALK = "player.walking";

    private void Update()
    {
        foreach (var c in components)
        {
            if (!c.target)
                c.target = c.snapshot.source;

            if (c.stats.direction.sqrMagnitude != 0)
            {
                var scale = c.target.localScale;
                if (c.stats.direction.x < 0) scale.x = -1 * Mathf.Abs(scale.x);
                else if (c.stats.direction.x > 0) scale.x = Mathf.Abs(scale.x);
                c.target.localScale = scale;

                if (c.snapshot.frames.Count < 1 || c.snapshot.chosenFile != WALK)
                    c.snapshot.Load(WALK);

                c.tt(WALK)
                    .Add(0.1f, () => c.snapshot.Step(1))
                    .Immutable();
            }
            else
            {
                c.tt(WALK).Reset();
                if (c.snapshot.frames.Count < 1 || c.snapshot.chosenFile != IDLE)
                    c.snapshot.Load(IDLE);
                c.snapshot.Step(1);
            }
        }
    }
}