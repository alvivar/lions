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
                var id = parts[0];

                var posDir = parts[1].Replace("b.", "").Split(';');
                var pos = posDir[0];
                var dir = posDir[1];

                var posxy = pos.Split(',');
                var posx = posxy[0];
                var posy = posxy[1];

                var dirxy = dir.Split(',');
                var dirx = dirxy[0];
                var diry = dirxy[1];

                BulletSystem
                    .GetBullet()
                    .Fire(
                        new Vector3(Bitf.Float(posx, 0), Bitf.Float(posy, 0), 0),
                        new Vector3(Bitf.Float(dirx, 0), Bitf.Float(diry, 0), 0));
            }
        }
    }
}