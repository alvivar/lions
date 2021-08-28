using System.Collections.Generic;
using UnityEngine;

// #jam
public class MultiplayerPuppetSystem : MonoBehaviour
{
    public static List<MultiplayerPuppet> components = new List<MultiplayerPuppet>();

    private void Update()
    {
        foreach (var c in components)
        {

        }
    }
}