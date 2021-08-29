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
            if (!c.connected)
                continue;

            if (c.queries.Count > 0)
            {
                var query = c.queries[0];
                c.queries.RemoveAt(0);

                c.bite.Send(query);
            }
        }
    }
}