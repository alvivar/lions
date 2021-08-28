using UnityEngine;

// #jam
public class MultiplayerPuppet : MonoBehaviour
{
    public Transform t;

    private void Start()
    {
        t = GetComponentInParent<Transform>();
    }

    private void OnEnable()
    {
        MultiplayerPuppetSystem.components.Add(this);
    }

    private void OnDisable()
    {
        MultiplayerPuppetSystem.components.Remove(this);
    }
}