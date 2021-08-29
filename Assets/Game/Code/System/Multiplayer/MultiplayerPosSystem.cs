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

                var px = Util.Flat(pos.x);
                var py = Util.Flat(pos.y);

                c.delay = 0.3f;
                c.server.queries.Add($"! p.{id} p.{px},{py}");
            }
        }
    }
}