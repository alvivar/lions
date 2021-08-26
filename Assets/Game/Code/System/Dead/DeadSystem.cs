using System.Collections.Generic;
using UnityEngine;

// #jam
public class DeadSystem : MonoBehaviour
{
    public static List<Dead> entities = new List<Dead>();

    private void Update()
    {
        foreach (var e in entities)
        {
            if (e.deads.Count > 0)
            {
                e.deads.RemoveAt(0);

                // Deactivate player

                // Dead body
            }
        }
    }
}