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
            var pos = c.target.transform.position;
            var eaz = c.target.eulerAngles.z;

            if (c.pos != pos)
            {
                c.pos = pos;

                var px = Util.Flat(pos.x, 4);
                var py = Util.Flat(pos.y, 4);

                c.delay = 0.1f;
                c.server.queries.Add($"! p.{id} p.{px},{py},{eaz}");
            }
        }
    }
}