using System.Collections.Generic;
using UnityEngine;

public class WalkerByKeyboardSystem : MonoBehaviour
{
    public static List<WalkerByKeyboard> walkers = new List<WalkerByKeyboard>();

    void Update()
    {
        foreach (var w in walkers)
            w.stats.direction = w.input.wasd;
    }
}