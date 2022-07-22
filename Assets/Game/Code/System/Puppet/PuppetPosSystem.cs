using System.Collections.Generic;
using UnityEngine;

// #jam
public class PuppetPosSystem : MonoBehaviour
{
    public static Dictionary<int, PuppetPos> puppets = new Dictionary<int, PuppetPos>();
    public static List<PuppetPos> components = new List<PuppetPos>();

    private void Update()
    {
        foreach (var c in components)
        {
            if (c.id < 0)
                continue;

            if (c.positionsByFrame.Count > 0)
            {
                var pos = c.positionsByFrame.Dequeue();
                c.transform.position = c.currentPos = pos;
            }
        }
    }

    public static void PushPos(int id, Vector3 position, float rotationZ)
    {
        PuppetPos p = null;
        puppets.TryGetValue(id, out p);

        if (!p)
        {
            p = components.Find(i => i.id < 0);
            puppets[id] = p;
        }

        p.positionsByFrame.Enqueue(position);
    }

    public static void SetPos(int id, Vector3 position, float rotationZ)
    {
        PuppetPos p = null;
        puppets.TryGetValue(id, out p);

        if (!p)
        {
            p = components.Find(i => i.id < 0);
            puppets[id] = p;

            p.id = id;
            p.transform.position = position;
            p.rotationTarget.localEulerAngles = new Vector3(0, 0, rotationZ);
        }

        p.lastPosition = p.transform.position;
        p.serverPosition = position;
        p.rotationZ = rotationZ;
        p.t = 0;
    }

    /// Original lerp when the multiplayer data arrived every 0.1s ticks.
    public void Lerp(PuppetPos pp)
    {
        pp.t += Time.deltaTime * 4;
        pp.currentPos = Vector3.Lerp(
            pp.currentPos,
            pp.serverPosition,
            pp.t);

        pp.transform.position = Vector3.Lerp(
            pp.transform.position,
            pp.currentPos,
            Time.deltaTime * 4);

        pp.rotationTarget.localEulerAngles = new Vector3(0, 0, pp.rotationZ);
    }
}