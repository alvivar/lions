using UnityEngine;

// #jam
public class MultiplayerId : MonoBehaviour
{
    public Transform t;

    private void Start()
    {
        t = GetComponent<Transform>();
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