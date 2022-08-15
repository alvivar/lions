using System;
using System.Collections.Generic;
using BiteClient;
using UnityEngine;

// #jam
public class MultiplayerServer : MonoBehaviour
{
    public Action<string> OnResponse;
    public Queue<string> queries = new Queue<string>();

    [Header("Network")]
    public long id = -1;
    public bool connected = false;
    public Bite bite;

    private void Start()
    {
        this.tt("TryConnection")
            .Add(() =>
            {
                // bite = new Bite("167.99.58.31", 1986);
                bite = new Bite("127.0.0.1", 1984);

                var uid = SystemInfo.deviceUniqueIdentifier;
                bite.Send($"! ping from {uid}", r =>
                {
                    connected = true;
                    Debug.Log("MultiplayerServer connected");
                });

                bite.FrameReceived += frame =>
                {
                    if (frame.Data.Length <= 5) // Let's ignore responses like OK or KEY? from queries.
                        return;

                    var message = frame.Text.Trim();
                    var lines = message.Split('\n');

                    Debug.Log($"Byte DataReceived ({frame.Size}): {string.Join(" ", frame.Data)}");
                    Debug.Log($"String DataReceived ({message.Length}): {message}");

                    // There is always a new line at the end of the message, and
                    // sometimes there are more than one message in the data.
                    foreach (var line in lines)
                    {
                        if (OnResponse != null && line.Length > 5)
                            OnResponse(line);
                    }
                };
            })
            .Add(3)
            .Wait(() => !connected, 3)
            .Add(() => bite.Close())
            .Repeat();

        this.tt("WaitForId")
            .Wait(() => connected)
            .Add(t => bite.Send($"+1 app.connections.id", frame =>
            {
                id = Bitf.Long(frame.Content);
                Debug.Log($"User ID: {id}");
            }));
    }

    private void OnDestroy()
    {
        if (bite != null)
            bite.Close();
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