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
            if (id < 0)
                continue;

            var pos = c.target.transform.position;
            pos.x = Bitf.Round(pos.x, 4);
            pos.y = Bitf.Round(pos.y, 4);

            if (Vector3.Distance(c.position, pos) > 0.0001f)
            {
                c.position = pos;
                c.rotationZ = c.target.eulerAngles.z;

                var px = Bitf.Str(c.position.x, 4);
                var py = Bitf.Str(c.position.y, 4);
                var rz = (int)c.rotationZ;

                c.delay = 0.1f;
                c.server.queries.Enqueue($"! p.{id} p{px},{py},{rz}");
            }
        }
    }
}