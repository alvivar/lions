using System;
using System.Collections.Generic;
using System.Text;
using BiteServer;
using UnityEngine;

// #jam
public class MultiplayerServer : MonoBehaviour
{
    public Action<string> OnResponse;
    public List<string> queries = new List<string>();

    [Header("Network")]
    public long id = -1;
    public bool connected = false;
    public Bite bite;

    private void Start()
    {
        var bite2 = new Bite();
        this.tt()
            .Add(1, () =>
            {
                bite2.Send("This");
                bite2.Send("That");
                bite2.Send("Wat?");
            });

        // this.tt("RetryConnection")
        //     .Add(() =>
        //     {
        //         bite = new Bite("142.93.180.20", 1986);

        //         bite.OnConnected += () =>
        //         {
        //             connected = true;
        //             Debug.Log("MultiplayerServer connected");
        //         };

        //         bite.OnError += msg =>
        //         {
        //             connected = false;
        //             Debug.Log($"MultiplayerServer disconnected: {msg}");
        //         };

        //         bite.OnResponse += msg =>
        //         {
        //             if (msg.Length < 6)
        //                 return;

        //             if (OnResponse != null)
        //                 OnResponse(msg);
        //         };
        //     })
        //     .Add(3).Loop(t =>
        //     {
        //         if (!connected)
        //             t.Break();

        //         t.Wait(1);
        //     })
        //     .Repeat();

        // this.tt("WaitForId")
        //     .Wait(() => connected)
        //     .If(() => id < 0)
        //     .Add(t => bite.Send($"+1 app.connections.id", x => id = Bite.Long(x, 0)));
    }

    private void OnEnable()
    {
        MultiplayerServerSystem.components.Add(this);
    }

    private void OnDisable()
    {
        MultiplayerServerSystem.components.Remove(this);
    }
}