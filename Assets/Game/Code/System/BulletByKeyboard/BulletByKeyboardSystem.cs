using System.Collections.Generic;
using UnityEngine;

// #jam
public class BulletByKeyboardSystem : MonoBehaviour
{
    public static List<BulletByKeyboard> entities = new List<BulletByKeyboard>();

    private void Update()
    {
        foreach (var e in entities)
        {
            if (e.delay > 0)
                e.delay -= Time.deltaTime;

            if (e.spaceDown < e.input.spaceDown)
            {
                e.spaceDown = e.input.spaceDown;

                if (e.delay <= 0)
                {
                    e.delay = 0.8f;

                    var bullet = BulletSystem.GetBullet();
                    bullet.transform.position = e.origin.transform.position;

                    bullet.trail.Clear();
                    bullet.trail.enabled = true;

                    bullet.stats.direction = e.input.lastWasd;
                    bullet.stats.duration = 0;
                }
            }
        }
    }
}