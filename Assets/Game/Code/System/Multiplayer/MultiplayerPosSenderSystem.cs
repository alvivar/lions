using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerPosSenderSystem : MonoBehaviour
{
    public static List<MultiplayerPosSender> components = new List<MultiplayerPosSender>();

    private void Update()
    {
        foreach (var c in components)
        {
            c.delay -= Time.deltaTime;
            if (c.delay > 0)
                continue;

            var id = c.server.id;
            var pos = c.transform.position;
            pos.x = Util.Round(pos.x, 3);
            pos.y = Util.Round(pos.y, 3);

            if (c.pos != pos)
            {
                c.pos = pos;

                var px = Util.Flat(pos.x, 3);
                var py = Util.Flat(pos.y, 3);

                c.delay = 0.1f;
                c.server.queries.Add($"! p.{id} p.{px},{py}");
            }
        }
    }
}