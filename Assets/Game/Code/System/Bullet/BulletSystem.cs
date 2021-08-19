using System.Collections.Generic;
using UnityEngine;

// #jam
public class BulletSystem : MonoBehaviour
{
    public static List<Bullet> entities = new List<Bullet>();
    public static int bulletIndex = 0;

    private void Update()
    {
        foreach (var e in entities)
        {
            if (e.stats.direction != Vector3.zero)
            {
                e.rbody.velocity = e.stats.speed * e.stats.direction;

                e.stats.duration += Time.deltaTime;
                if (e.stats.duration > 4)
                {
                    e.stats.duration = 0;
                    e.stats.direction = Vector3.zero;
                }
            }
            else
            {
                e.rbody.velocity = Vector3.zero;
                e.rbody.angularVelocity = Vector3.zero;
            }
        }
    }

    public static Bullet GetBullet()
    {
        var bullet = entities[bulletIndex];
        bulletIndex = ++bulletIndex % entities.Count;

        return bullet;
    }
}