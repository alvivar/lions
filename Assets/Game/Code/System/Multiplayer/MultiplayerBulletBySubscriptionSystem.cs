using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerBulletBySubscriptionSystem : MonoBehaviour
{
    public static List<MultiplayerBulletBySubscription> components = new List<MultiplayerBulletBySubscription>();

    private void Update()
    {
        foreach (var c in components)
        {

        }
    }
}