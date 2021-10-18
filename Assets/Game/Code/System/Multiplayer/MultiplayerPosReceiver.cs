using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerPosReceiver : MonoBehaviour
{
    public struct Data { public string id; public string position; }
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

            positions.Enqueue(new Data { id = parts[0], position = parts[1] });
        };

        this.tt("WaitSubscribe")
            .Wait(() => server.id > 0)
            .Add(() => server.bite.Send("#b p", r => Debug.Log($"Positions subscription")))
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