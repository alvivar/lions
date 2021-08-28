using UnityEngine;

// #jam
public class MultiplayerId : MonoBehaviour
{
    public int id = -1;

    public Transform t;

    private void Start()
    {
        t = GetComponentInParent<Transform>();
    }

    private void OnEnable()
    {
        MultiplayerIdSystem.components.Add(this);
    }

    private void OnDisable()
    {
        MultiplayerIdSystem.components.Remove(this);
    }
}