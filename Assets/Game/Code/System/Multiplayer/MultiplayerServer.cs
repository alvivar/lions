using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerServer : MonoBehaviour
{
    public List<string> queries = new List<string>();

    [Header("Network")]
    public long id = -1;
    public bool connected = false;
    public Bite bite;

    private void Start()
    {
        this.tt("RetryConnection")
            .Add(() =>
            {
                bite = new Bite("142.93.180.20", 1986);
                bite.OnConnected += OnConnected;
                bite.OnError += OnError;
            })
            .Add(3).Loop(t =>
            {
                if (!connected)
                    t.Break();

                t.Wait(1);
            })
            .Repeat();

        this.tt("WaitForId")
            .Wait(() => connected)
            .If(() => id < 0)
            .Add(t => bite.Send($"+1 app.connections.id", x => id = Bite.Long(x, 0)));
    }

    private void OnConnected()
    {
        connected = true;

        Debug.Log("MultiplayerId connected!");
    }

    private void OnError(string message)
    {
        connected = false;

        Debug.Log($"MultiplayerId disconnected: {message}");
    }

    private void OnEnable()
    {
        MultiplayerServerSystem.components.Add(this);
    }

    private void OnDisable()
    {
        MultiplayerServerSystem.components.Remove(this);

        if (bite != null && connected)
            bite.Close();
    }
}