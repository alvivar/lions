using UnityEngine;
using BiteClient;
using System.Text;

public class SendMaximumBytes : MonoBehaviour
{
    public string command = "s SendMaximumBytes ";
    public int amount = 65535; // The missing 10 bytes are the query.

    private Bite bite;
    private bool connected = false;

    void Start()
    {
        // bite = new Bite("167.99.58.31", 1986);
        bite = new Bite("127.0.0.1", 1984);

        var uid = SystemInfo.deviceUniqueIdentifier;
        bite.Send($"! ping from {uid}", r =>
        {
            connected = true;
            Debug.Log("SendMaximumBytes connected");
        });

        bite.FrameReceived += frame =>
        {
            var message = frame.Text.Trim();

            Debug.Log($"FrameReceived Bytes ({frame.Size}): {string.Join(" ", frame.Data)}");
            Debug.Log($"FrameReceived String ({message.Length}): {message}");
        };
    }

    void OnDisable()
    {
        bite.Close();
    }

    [ContextMenu("Send the maximum ammount of bytes")]
    public void SendMaxBytes()
    {
        bite.Send($"#g SendMaximumBytes", frame =>
        {
            Debug.Log("#g SendMaximumBytes received");

            var message = frame.Text.Trim();

            Debug.Log($"#g SendMaximumBytes Bytes ({frame.Size}): {string.Join(" ", frame.Data)}");
            Debug.Log($"#g SendMaximumBytes String ({message.Length}): {message}");
        });

        var builder = new StringBuilder();
        var ascii = 65;

        for (int i = 0; i < amount; i++)
        {
            ascii = ascii > 90 ? 65 : ascii;
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
