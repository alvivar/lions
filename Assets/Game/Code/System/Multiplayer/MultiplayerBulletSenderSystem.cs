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

                var px = Util.Flat(pos.x, 4);
                var py = Util.Flat(pos.y, 4);

                var dx = Util.Flat(dir.x, 4);
                var dy = Util.Flat(dir.y, 4);

                c.server.queries.Enqueue($"! b.{id} b:{px},{py};{dx},{dy}");
            }
        }
    }
}