using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerBulletReceiverSystem : MonoBehaviour
{
    public static List<MultiplayerBulletReceiver> components = new List<MultiplayerBulletReceiver>();

    private void Update()
    {
        foreach (var c in components)
        {
            while (c.bullets.Count > 0)
            {
                var bullet = c.bullets[0];
                c.bullets.RemoveAt(0);

                var parts = bullet.Split(' ');
                var id = Bitf.Float(parts[0], -1);

                var posDir = parts[1].Replace("b", "").Split(',');
                var posx = posDir[0];
                var posy = posDir[1];
                var dirx = posDir[2];
                var diry = posDir[3];

                var layer = id == c.server.id ? c.playerLayer : c.enemyLayer;

                BulletSystem
                    .GetBullet(layer)
                    .Fire(
                        new Vector3(Bitf.Float(posx, 0), Bitf.Float(posy, 0)),
                        new Vector3(Bitf.Float(dirx, 0), Bitf.Float(diry, 0)));
            }
        }
    }
}