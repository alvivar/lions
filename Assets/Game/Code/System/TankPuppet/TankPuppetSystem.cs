using System.Collections.Generic;
using UnityEngine;

// #jam
public class TankPuppetSystem : MonoBehaviour
{
    public static Dictionary<int, TankPuppet> puppets = new Dictionary<int, TankPuppet>();
    public static List<TankPuppet> components = new List<TankPuppet>();

    private void Update()
    {
        foreach (var c in components)
        {
            if (c.id < 0)
                continue;

            var dir = (c.serverPosition - c.transform.position).normalized;
            c.stats.direction = dir;

            // c.transform.position = Vector3.Lerp(
            //     c.transform.position,
            //     c.serverPosition,
            //     Time.deltaTime);
        }
    }

    public static void SetPos(int id, Vector3 position)
    {
        TankPuppet puppet = null;
        puppets.TryGetValue(id, out puppet);

        if (!puppet)
        {
            puppet = components.Find(i => i.id < 0);
            puppet.id = id;
            puppets[id] = puppet;
        }

        puppet.serverPosition = position;
    }
}