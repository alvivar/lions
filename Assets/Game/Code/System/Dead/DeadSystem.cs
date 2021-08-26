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

                this.tt("Dead").Reset().Add(0.5f, () =>
                {
                    var deadTank = DeadTankSystem.GetDeadTank();
                    deadTank.SetPosition(e.transform.position);
                    e.transform.position += Vector3.one * 9999;
                });
            }
        }
    }
}