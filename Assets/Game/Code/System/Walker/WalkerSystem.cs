using System.Collections.Generic;
using UnityEngine;

// #jam
public class WalkerSystem : MonoBehaviour
{
    public static List<Walker> entities = new List<Walker>();

    private void FixedUpdate()
    {
        foreach (var e in entities)
        {
            var stats = e.stats;

            var speed = e.stats.speedOverride > e.stats.speed ? e.stats.speedOverride : e.stats.speed;
            var dir = speed * e.stats.direction;
            var damp = e.stats.dampening * Time.deltaTime;

            e.rbody.velocity = Vector3.Lerp(e.rbody.velocity, dir, damp);
            e.rbody.angularVelocity = 0;
            e.rbody.transform.localEulerAngles = Vector3.zero;
        }
    }
}