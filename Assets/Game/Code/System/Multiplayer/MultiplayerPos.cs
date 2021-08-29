using UnityEngine;

// #jam
public class MultiplayerPos : MonoBehaviour
{
    public Vector3 pos;
    public float delay;

    [Header("By GetComponent")]
    public MultiplayerServer server;

    private void Start()
    {
        server = GetComponentInParent<MultiplayerServer>();
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