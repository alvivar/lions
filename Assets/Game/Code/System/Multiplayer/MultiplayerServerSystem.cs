using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerServerSystem : MonoBehaviour
{
    public static List<MultiplayerServer> components = new List<MultiplayerServer>();

    private void Update()
    {
        foreach (var c in components)
        {
            if (c.id < 0)
                continue;

            while (c.queries.Count > 0)
            {
                var query = c.queries.Dequeue();
                c.bite.Send(query);
            }
        }
    }
}