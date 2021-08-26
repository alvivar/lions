using System.Collections.Generic;
using UnityEngine;

// #jam
public class DeadTankSystem : MonoBehaviour
{
    public static List<DeadTank> entities = new List<DeadTank>();
    public static int index = 0;

    private void Update()
    {
        foreach (var e in entities)
        {
            if (e.positions.Count > 0)
            {
                var pos = e.positions[0];
                e.positions.RemoveAt(0);

                foreach (var r in e.rigidBodies)
                {
                    r.transform.position = pos + Random.insideUnitSphere;
                    r.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));

                    var dir = (r.transform.position - pos).normalized;
                    r.AddForceAtPosition(dir * 4, pos, ForceMode2D.Impulse);
                    r.angularVelocity = 0;
                }
            }
        }
    }

    public static DeadTank GetDeadTank()
    {
        var deadTank = entities[index];
        index = ++index % entities.Count;

        return deadTank;
    }
}