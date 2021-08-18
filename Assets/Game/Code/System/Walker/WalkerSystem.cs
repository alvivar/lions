using System.Collections.Generic;
using UnityEngine;

public class WalkerSystem : MonoBehaviour
{
    public static List<Walker> walkers = new List<Walker>();

    private void Update()
    {
        foreach (var walker in walkers)
        {
            var stats = walker.stats;
            walker.transform.localPosition += stats.direction * stats.speed * Time.deltaTime;
        }
    }
}