using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerBulletSenderSystem : MonoBehaviour
{
    public static List<MultiplayerBulletSender> components = new List<MultiplayerBulletSender>();

    private void Update()
    {
        foreach (var c in components)
        {
            if (c.bullets.Count > 0)
            {
                var bullet = c.bullets[0];
                c.bullets.RemoveAt(0);

                var id = c.server.id;
                var pos = bullet.pos;
                var dir = bullet.dir;

                var px = Bitf.Str(pos.x, 4);
                var py = Bitf.Str(pos.y, 4);

                var dx = Bitf.Str(dir.x, 0);
                var dy = Bitf.Str(dir.y, 0);

                c.server.queries.Enqueue($"! b.{id} b{px},{py},{dx},{dy}");
            }
        }
    }
}