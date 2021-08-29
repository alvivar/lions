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

                var px = Util.Flat(pos.x);
                var py = Util.Flat(pos.y);

                var dx = Util.Flat(dir.x);
                var dy = Util.Flat(dir.y);

                c.server.queries.Add($"! b.{id} b.{px},{py};{dx},{dy}");
            }
        }
    }
}