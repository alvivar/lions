using UnityEngine;

// #jam
public class Slowdown : MonoBehaviour
{
    private void OnEnable()
    {
        SlowdownSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        SlowdownSystem.entities.Remove(this);
    }
}