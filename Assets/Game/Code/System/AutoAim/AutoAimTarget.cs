using UnityEngine;

// #jam
public class AutoAimTarget : MonoBehaviour
{
    private void OnEnable()
    {
        AutoAimSystem.targets.Add(this);
    }

    private void OnDisable()
    {
        AutoAimSystem.targets.Remove(this);
    }
}