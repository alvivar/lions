using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerPosReceiver : MonoBehaviour
{
    public struct Pos { public string id; public string position; }
    public List<Pos> positions = new List<Pos>();

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

            positions.Add(new Pos { id = parts[0], position = parts[1] });
        };

        this.tt("Wait/Subscribe")
            .Wait(() => server.connected)
            .Add(() => server.bite.Send("#b p", r => Debug.Log($"Positions subscription")));
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