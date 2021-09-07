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
        this.tt("RetryConnection")
            .Add(() =>
            {
                bite = new Bite("142.93.180.20", 1986);

                bite.Send($"Ping: {this.GetInstanceID()}", r =>
                {
                    connected = true;
                    Debug.Log("MultiplayerServer connected");
                });

                bite.DataReceived += data =>
                {
                    if (data.Length < 6)
                        return;

                    if (OnResponse != null)
                        OnResponse(data);
                };
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
            .Add(t => bite.Send($"+1 app.connections.id", r =>
            {
                id = Bitf.Long(r, 0);
                Debug.Log($"ID received");
            }));
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