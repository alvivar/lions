using UnityEngine;

// #jam
public class MultiplayerPos : MonoBehaviour
{
    public Transform t;

    private void Start()
    {
        t = GetComponentInParent<Transform>();
    }

    private void OnEnable()
    {
        MultiplayerPosSystem.components.Add(this);
    }

    private void OnDisable()
    {
        MultiplayerPosSystem.components.Remove(this);
    }
}