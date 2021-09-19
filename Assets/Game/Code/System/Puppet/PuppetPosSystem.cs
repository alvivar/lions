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

            c.t += Time.deltaTime * 4.8f; // !
            c.transform.position = Vector3.Lerp(
                c.lastPosition,
                c.serverPosition,
                c.t);

            c.rotationTarget.transform.localEulerAngles = new Vector3(0, 0, c.rotationZ);
        }
    }

    public static void SetPos(int id, Vector3 position, float rotationZ)
    {
        PuppetPos p = null;
        puppets.TryGetValue(id, out p);

        if (!p)
        {
            p = components.Find(i => i.id < 0);
            p.id = id;
            puppets[id] = p;
        }

        p.lastPosition = p.transform.position;
        p.serverPosition = position;
        p.rotationZ = rotationZ;
        p.t = 0;
    }
}