using System.Collections.Generic;
using UnityEngine;

// #jam
public class SlowdownSystem : MonoBehaviour
{
    public static List<Slowdown> entities = new List<Slowdown>();

    private void Update()
    {
        foreach (var e in entities)
        {
            e.rigidBody.velocity = Vector2.Lerp(
                e.rigidBody.velocity,
                Vector2.zero,
                Time.deltaTime * 4);

            e.rigidBody.angularVelocity = Mathf.Lerp(
                e.rigidBody.angularVelocity,
                0,
                Time.deltaTime * 4);
        }
    }
}