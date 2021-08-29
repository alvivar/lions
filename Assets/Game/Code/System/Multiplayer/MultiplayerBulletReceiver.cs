using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerBulletReceiver : MonoBehaviour
{
    public List<string> bullets = new List<string>();

    [Header("By GetComponent")]
    public MultiplayerServer server;

    private void Start()
    {
        server = GetComponent<MultiplayerServer>();
        server.OnResponse += msg =>
        {
            var parts = msg.Split(' ');

            if (parts.Length < 2)
                return;

            if (parts[1].Substring(0, 1) != "b")
                return;

            bullets.Add(msg);
        };

        this.tt("WaitToSubscribe")
            .Wait(() => server.connected)
            .Add(() => server.bite.Send("#b b", msg => Debug.Log($"Subscribed to bullets")));
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