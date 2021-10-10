using UnityEngine;

// #jam
public class Cam : MonoBehaviour
{
    public Vector2 xLimit = new Vector2(-28.37f, 28.37f);
    public Vector2 yLimit = new Vector2(-15.93f, 15.93f);

    private void OnEnable()
    {
        CamSystem.cams.Add(this);
    }

    private void OnDisable()
    {
        CamSystem.cams.Remove(this);
    }
}