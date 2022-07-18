using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerPosSenderSystem : MonoBehaviour
{
    public static List<MultiplayerPosSender> components = new List<MultiplayerPosSender>();

    private string buffer = "";

    private void Update()
    {
        foreach (var c in components)
        {
            c.delay -= Time.deltaTime;

            var id = c.server.id;
            if (id < 0)
                continue;

            var pos = c.target.transform.position;
            pos.x = Bitf.Round(pos.x, 4);
            pos.y = Bitf.Round(pos.y, 4);

            if (Vector3.Distance(c.position, pos) > 0.0001f)
            {
                c.position = pos;
                c.rotationZ = c.target.eulerAngles.z;

                var px = Bitf.Str(c.position.x, 4);
                var py = Bitf.Str(c.position.y, 4);
                var rz = (int)c.rotationZ;

                buffer += $"{px},{py},{rz}|";

                if (c.delay < 0)
                {
                    c.delay = 0.1f;

                    var query = $"! p.{id} p{buffer}";
                    buffer = "";

                    Debug.Log(query);
                    c.server.queries.Enqueue(query);
                }
            }
        }
    }
}