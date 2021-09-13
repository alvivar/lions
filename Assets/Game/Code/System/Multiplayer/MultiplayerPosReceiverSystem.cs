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
                var msg = c.positions.Dequeue();

                var id = Bitf.Int(msg.id, -1);

                if (id == c.server.id)
                    continue;

                var parts = msg.position.Replace("p.", "").Split(',');
                var x = Bitf.Float(parts[0], -1);
                var y = Bitf.Float(parts[1], -1);

                TankPuppetSystem.SetPos(id, new Vector3(x, y, 0));
            }
        }
    }
}