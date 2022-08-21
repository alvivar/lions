using UnityEngine;
using BiteClient;
using System.Text;

public class BiteTest : MonoBehaviour
{
    public string command = "s test ";
    public string subscription = "#g test";

    [Header("Network")]
    public string host = "127.0.0.1";
    public int port = 1984;

    private Bite bite;
    private bool connected = false;

    void Start() { Connect(); }
    void OnDisable() { bite.Shutdown(); }

    public void Connect()
    {
        bite = new Bite(host, port);

        var uid = SystemInfo.deviceUniqueIdentifier;
        bite.Send($"! ping from {uid}", r =>
        {
            connected = true;

            SendMaxBytes();

            Debug.Log("SendMaximumBytes connected");
        });

        bite.FrameReceived += frame =>
        {
            var message = frame.Text.Trim();

            Debug.Log($"FrameReceived Bytes ({frame.Size}): {string.Join(" ", frame.Data)}");
            Debug.Log($"FrameReceived String ({message.Length}): {message}");
        };
    }

    public void SendMaxBytes()
    {
        bite.Send($"{subscription}", frame =>
        {
            Debug.Log("{subscription} received");

            var message = frame.Text.Trim();

            Debug.Log($"{subscription} Bytes ({frame.Size}): {string.Join(" ", frame.Data)}");
            Debug.Log($"{subscription} String ({message.Length}): {message}");
        });

        var from = 0;
        var to = 255;
        var ascii = 0;

        var max = 65535;
        var commandSize = command.Length + 2;

        var builder = new StringBuilder();
        for (int i = 0; i < max - commandSize; i++)
        {
            ascii = ascii > to ? from : ascii;
            builder.Append((char)ascii);
            ascii += 1;
        }

        string content = builder.ToString();
        Debug.Log($"{command} content ({content.Length}): {content}");

        bite.Send($"{command}{content}", frame =>
        {
            var message = frame.Text.Trim();

            Debug.Log($"{command} Bytes ({frame.Size}): {string.Join(" ", frame.Data)}");
            Debug.Log($"{command} String ({message.Length}): {message}");
        });
    }
}
