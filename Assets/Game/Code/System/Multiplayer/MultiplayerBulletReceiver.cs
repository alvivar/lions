using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerBulletReceiver : MonoBehaviour
{
    public string playerLayer = "player";
    public string enemyLayer = "enemy";

    [Header("Queue")]
    public Queue<string> bullets = new Queue<string>();

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

            bullets.Enqueue(data);
        };

        this.tt("WaitSubscribe")
            .Wait(() => server.id > 0)
            .Add(() => server.bite.Send("#k b", r => Debug.Log($"Bullets subscription")))
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