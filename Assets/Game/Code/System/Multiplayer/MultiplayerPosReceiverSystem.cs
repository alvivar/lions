using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerPosReceiverSystem : MonoBehaviour
{
    public static List<MultiplayerPosReceiver> components = new List<MultiplayerPosReceiver>();

    private void Update()
    {
        foreach (var c in components)
        {
            if (c.positions.Count > 0)
            {
                var data = c.positions.Dequeue();

                var id = Bitf.Int(data.id, -1);

                if (id == c.server.id)
                    continue;

                // @todo There should be a buffer over here, because each
                // positions, represents a frame.

                foreach (var pos in data.positions)
                {
                    if (pos.Length < 1)
                        continue;

                    Debug.Log($"MultiplayerPosReceiverSystem: {pos} / {pos.Length}");

                    var parts = pos.Split(',');
                    var x = Bitf.Float(parts[0], -1); // pos.x
                    var y = Bitf.Float(parts[1], -1); // pos.y
                    var z = Bitf.Float(parts[2], -1); // euler.z

                    PuppetPosSystem.PushPos(id, new Vector3(x, y), z);
                }
            }
        }
    }
}