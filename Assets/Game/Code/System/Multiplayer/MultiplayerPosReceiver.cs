using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerPosReceiver : MonoBehaviour
{
    public struct Data { public string id; public List<string> positions; }
    public Queue<Data> positions = new Queue<Data>();

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

            if (parts[1].Substring(0, 1) != "p")
                return;

            Debug.Log($"MultiplayerPosReceiver: {parts[1].Substring(1)}");

            var poses = new List<string>(parts[1].Substring(1).Split('|'));
            positions.Enqueue(new Data { id = parts[0], positions = poses });
        };

        this.tt("Wait&Subscribe")
            .Wait(() => server.id > 0)
            .Add(() => server.bite.Send("#k p", r => Debug.Log($"Positions subscription")))
            .Wait(() => !server.connected, 1)
            .Repeat();
    }

    private void OnEnable()
    {
        MultiplayerPosReceiverSystem.components.Add(this);
    }

    private void OnDisable()
    {
        MultiplayerPosReceiverSystem.components.Remove(this);
    }
}