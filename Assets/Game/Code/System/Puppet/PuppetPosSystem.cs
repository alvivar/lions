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

            c.t += Time.deltaTime * 4.8f;
            c.transform.position = Vector3.Lerp(
                c.lastPosition,
                c.serverPosition,
                c.t);
        }
    }

    public static void SetPos(int id, Vector3 position)
    {
        PuppetPos puppet = null;
        puppets.TryGetValue(id, out puppet);

        if (!puppet)
        {
            puppet = components.Find(i => i.id < 0);
            puppet.id = id;
            puppets[id] = puppet;
        }

        puppet.lastPosition = puppet.transform.position;
        puppet.serverPosition = position;
        puppet.lastDirection = (puppet.serverPosition - puppet.lastDirection).normalized;
        puppet.t = 0;
    }
}