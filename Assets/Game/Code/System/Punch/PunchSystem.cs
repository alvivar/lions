using System.Collections.Generic;
using UnityEngine;

// #jam
public class PunchSystem : MonoBehaviour
{
    public static List<Punch> entities = new List<Punch>();

    private void Update()
    {
        foreach (var e in entities)
        {
            if (e.force.magnitude > 0)
            {
                e.rbody.velocity += e.force;
                e.force = Vector2.Lerp(e.force, Vector2.zero, Time.deltaTime * 12);
            }
        }
    }
}