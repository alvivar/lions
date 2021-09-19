using UnityEngine;

// #jam
public class PuppetPos : MonoBehaviour
{
    public int id = -1;
    public Vector3 lastPosition = Vector3.one * -9999;
    public Vector3 serverPosition = Vector3.one * -9999;
    public Vector3 lastDirection = Vector3.one * -9999;
    public float t = 0;

    [Header("By GetComponent")]
    public Stats stats;

    private void Start()
    {
        stats = GetComponent<Stats>();
    }

    private void OnEnable()
    {
        PuppetPosSystem.components.Add(this);
    }

    private void OnDisable()
    {
        PuppetPosSystem.components.Remove(this);
    }
}