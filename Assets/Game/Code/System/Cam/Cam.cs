using UnityEngine;

// #jam
public class Cam : MonoBehaviour
{
    private void OnEnable()
    {
        CamSystem.cams.Add(this);
    }

    private void OnDisable()
    {
        CamSystem.cams.Remove(this);
    }
}