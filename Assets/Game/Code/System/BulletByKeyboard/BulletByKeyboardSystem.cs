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

                    var pos = e.origin.transform.position;
                    var dir = e.input.lastWasd;

                    // BulletSystem
                    //     .GetBullet()
                    //     .Fire(pos, dir);

                    if (e.OnBullet != null)
                        e.OnBullet(pos, dir);
                }
            }
        }
    }
}