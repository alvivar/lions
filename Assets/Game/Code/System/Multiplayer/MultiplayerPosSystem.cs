using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerPosSystem : MonoBehaviour
{
    public static List<MultiplayerPos> components = new List<MultiplayerPos>();

    private void Update()
    {
        foreach (var c in components)
        {
            c.delay -= Time.deltaTime;
            if (c.delay > 0)
                continue;

            var id = c.server.id;
            var pos = c.transform.position;
            if (c.pos != pos)
            {
                c.pos = pos;

                var nx = (int) pos.x;
                var ny = (int) pos.y;
                var dx = (int) Mathf.Abs((pos.x - nx) * 1000);
                var dy = (int) Mathf.Abs((pos.y - ny) * 1000);

                c.delay = 0.3f;
                c.server.queries.Add($"s p.{id} {nx}.{dx},{ny}.{dy}");
            }
        }
    }
}