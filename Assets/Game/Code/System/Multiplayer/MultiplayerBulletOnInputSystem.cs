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

                c.server.queries.Add($"s b.{id} {Flat(pos.x)},{Flat(pos.y)};{Mathf.Round(dir.x)},{Mathf.Round(dir.y)}");
            }
        }
    }

    public static string Flat(float f)
    {
        var nf = (int) f;
        var df = (int) Mathf.Abs((f - nf) * 1000);

        return $"{nf}.{df}";
    }
}