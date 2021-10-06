using UnityEngine;

// #jam
public class CamTarget : MonoBehaviour
{
    private void OnEnable()
    {
        CamSystem.targets.Add(this);
    }

    private void OnDisable()
    {
        CamSystem.targets.Remove(this);
    }
}