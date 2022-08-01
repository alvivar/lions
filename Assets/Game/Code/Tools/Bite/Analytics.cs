using System;
using UnityEngine;
using BiteClient;

[System.Serializable]
public struct AnalyticsData
{
    public string name;
    public int timePlayed;
    public Vector3 lastPosition;
    public long lastEpoch;
    public long startedEpoch;
}

internal struct Pos { public float x; public float y; public float z; }

public class Analytics : MonoBehaviour
{
    public string keyName = "team.game.user";

    [Header("Server")]
    public string host = "167.99.58.31";
    public int port = 1986;

    [Header("Info")]
    public string id; // SystemInfo.deviceUniqueIdentifier
    public string key;
    public AnalyticsData data;

    [Header("Config")]
    public int tick = 3;
    public float clock = 0;

    [Header("Optional")]
    public Transform position;

    private bool connected = false;
    private bool lastPositionLoaded = false;

    private Bite bite;

    private void Start()
    {
        id = SystemInfo.deviceUniqueIdentifier;
        key = $"{keyName}.{id}";

        bite = new Bite(host, port);
        bite.DataReceived += OnDataReceived;
    }

    private void Update()
    {
        // Wait for connection.
        if (!connected)
            return;

        // Tick
        if (clock > Time.time)
            return;
        clock = Time.time + tick;

        // Statistics
        SaveTimePlayed(tick);
        SaveLastEpoch();
        SaveLastPosition();
    }

    void OnDestroy()
    {
        if (bite != null)
            bite.Close();
    }

    void OnError(string error)
    {
        Debug.Log($"Analytics error: {error}");
    }

    private void OnDataReceived(byte[] response)
    {
        OnConnected();
    }

    void OnConnected()
    {
        connected = true;
        LoadDataFromServer();
        LoadOrSetStartedEpoch();
        Debug.Log($"Analytics connected");
    }

    void LoadDataFromServer()
    {
        bite.Send($"g {key}.name", response =>
        {
            var message = Bitf.Str(response);
            if (message.Trim().Length < 1)
                message = "?";

            data.name = message;
        });

        bite.Send($"g {key}.timePlayed", response =>
        {
            data.timePlayed = Bitf.Int(response);
        });

        bite.Send($"j {key}.lastPosition", response =>
        {
            var message = Bitf.Str(response);
            var json = JsonUtility.FromJson<Pos>(message);

            data.lastPosition = new Vector3(
                Bitf.Float($"{json.x}", 0),
                Bitf.Float($"{json.y}", 0),
                Bitf.Float($"{json.z}", 0));

            lastPositionLoaded = true;
        });
    }

    void SaveTimePlayed(int time)
    {
        if (data.timePlayed < 0) // Wait to be loaded for the first time.
            return;

        data.timePlayed += time;
        bite.Send($"s {key}.timePlayed {data.timePlayed}");
    }

    void SaveLastEpoch()
    {
        data.lastEpoch = DateTimeOffset.Now.ToUnixTimeSeconds();
        bite.Send($"s {key}.lastEpoch {data.lastEpoch}");
    }

    void LoadOrSetStartedEpoch()
    {
        bite.Send($"g {key}.startedEpoch", response =>
        {
            data.startedEpoch = Bitf.Long(response);

            if (data.startedEpoch <= 0)
            {
                data.startedEpoch = DateTimeOffset.Now.ToUnixTimeSeconds();
                bite.Send($"s {key}.startedEpoch {data.startedEpoch}");
            }
        });
    }

    void SaveLastPosition()
    {
        if (!position || !lastPositionLoaded)
            return;

        if (data.lastPosition == position.transform.position)
            return;

        data.lastPosition = position.transform.position;

        // Multiples sets in one send.
        var x = $"s {key}.lastPosition.x {data.lastPosition.x}\n";
        var y = $"s {key}.lastPosition.y {data.lastPosition.y}\n";
        var z = $"s {key}.lastPosition.z {data.lastPosition.z}";
        bite.Send($"{x}{y}{z}");
    }

    public void SetName(string name)
    {
        data.name = name;
        bite.Send($"s {key}.name {data.name}");
    }
}
