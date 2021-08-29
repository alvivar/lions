using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerBulletOnInputSystem : MonoBehaviour
{
    public static List<MultiplayerBulletOnInput> components = new List<MultiplayerBulletOnInput>();

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

                c.server.queries.Add($"s b.{id} {px},{py};{dx},{dy}");
            }
        }
    }
}