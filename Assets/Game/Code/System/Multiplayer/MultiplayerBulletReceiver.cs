using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerBulletReceiver : MonoBehaviour
{
    public string playerLayer = "player";
    public string enemyLayer = "enemy";

    [Header("Queue")]
    public List<string> bullets = new List<string>();

    [Header("By GetComponent")]
    public MultiplayerServer server;

    private void Start()
    {
        server = GetComponent<MultiplayerServer>();
        server.OnResponse += data =>
        {
            var parts = data.Split(' ');

            if (parts.Length < 2)
                return;

            if (parts[1].Substring(0, 1) != "b")
                return;

            bullets.Add(data);
        };

        this.tt("Wait/Subscribe")
            .Wait(() => server.connected)
            .Add(() => server.bite.Send("#b b", r => Debug.Log($"Bullets subscription")))
            .Wait(() => !server.connected, 1)
            .Repeat();
    }

    private void OnEnable()
    {
        MultiplayerBulletReceiverSystem.components.Add(this);
    }

    private void OnDisable()
    {
        MultiplayerBulletReceiverSystem.components.Remove(this);
    }
}