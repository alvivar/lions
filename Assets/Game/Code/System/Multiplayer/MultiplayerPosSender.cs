using UnityEngine;

// #jam
public class MultiplayerPosSender : MonoBehaviour
{
    public Vector3 position;
    public float rotationZ;
    public float delay;

    [Header("Required")]
    public Transform target;

    [Header("By GetComponent")]
    public MultiplayerServer server;

    private void Start()
    {
        server = GetComponentInParent<MultiplayerServer>();
    }

    private void OnEnable()
    {
        MultiplayerPosSenderSystem.components.Add(this);
    }

    private void OnDisable()
    {
        MultiplayerPosSenderSystem.components.Remove(this);
    }
}