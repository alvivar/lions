using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerBulletOnInput : MonoBehaviour
{
    public struct msg { public Vector3 pos; public Vector3 dir; }
    public List<msg> bullets = new List<msg>();

    public MultiplayerServer server;
    public BulletByKeyboard bulletByKeyboard;

    private void Start()
    {
        server = GetComponentInParent<MultiplayerServer>();
        bulletByKeyboard = GetComponentInParent<BulletByKeyboard>();
        bulletByKeyboard.OnBullet += (pos, dir) => bullets.Add(new msg { pos = pos, dir = dir });
    }

    private void OnEnable()
    {
        MultiplayerBulletOnInputSystem.components.Add(this);
    }

    private void OnDisable()
    {
        MultiplayerBulletOnInputSystem.components.Remove(this);
    }
}