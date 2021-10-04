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
                var bullet = c.bullets.Dequeue();

                var parts = bullet.Split(' ');
                var id = Bitf.Float(parts[0], -1);

                var posDir = parts[1].Replace("b", "").Split(',');
                var posx = Bitf.Float(posDir[0], 0);
                var posy = Bitf.Float(posDir[1], 0);
                var dirx = Bitf.Float(posDir[2], 0);
                var diry = Bitf.Float(posDir[3], 0);

                var layer = id == c.server.id ? c.playerLayer : c.enemyLayer;

                BulletSystem.GetBullet().ToLayer(layer).Fire(
                    new Vector3(posx, posy),
                    new Vector3(dirx, diry));
            }
        }
    }
}