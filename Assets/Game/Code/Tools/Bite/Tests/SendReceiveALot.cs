using UnityEngine;
using BiteClient;
using System.Text;

public class SendReceiveALot : MonoBehaviour
{
    public string command = "s test ";
    public int amount = 65526; // The missing 10 bytes are the query.

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
            Debug.Log("SendReceiveALot connected");
        });

        bite.FrameReceived += frame =>
        {
            var message = frame.Text.Trim();

            Debug.Log($"Byte received ({frame.Size}): {string.Join(" ", frame.Data)}");
            Debug.Log($"String received ({message.Length}): {message}");
        };
    }

    [ContextMenu("Send the maximum ammount of bytes")]
    public void SendLotsOfBytes()
    {
        bite.Send($"#g test", frame =>
        {
            Debug.Log("#g test received");

            var message = frame.Text.Trim();

            Debug.Log($"Test1 # Byte received ({frame.Size}): {string.Join(" ", frame.Data)}");
            Debug.Log($"Test1 # String received ({message.Length}): {message}");
        });

        var builder = new StringBuilder();
        var index = 65;

        for (int i = 0; i < amount; i++)
        {
            index = index > 90 ? 65 : index;
            builder.Append((char)index);
            index += 1;
        }

        string content = builder.ToString();
        Debug.Log($"Test1 content ({content.Length}): {content}");

        bite.Send($"{command}{content}", frame =>
        {
            var message = frame.Text.Trim();

            Debug.Log($"Test1 # Byte received ({frame.Size}): {string.Join(" ", frame.Data)}");
            Debug.Log($"Test1 # String received ({message.Length}): {message}");
        });
    }
}
