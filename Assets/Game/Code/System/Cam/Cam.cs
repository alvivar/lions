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

    [ContextMenu("Set Limits")]
    public void SetLimits()
    {
        var x = transform.position.x;
        xLimit.x = -1 * Mathf.Abs(transform.position.x);
        xLimit.y = Mathf.Abs(transform.position.x);

        var y = transform.position.y;
        yLimit.x = -1 * Mathf.Abs(transform.position.y);
        yLimit.y = Mathf.Abs(transform.position.y);
    }
}