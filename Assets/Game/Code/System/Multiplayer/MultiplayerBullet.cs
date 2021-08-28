using UnityEngine;

// #jam
public class MultiplayerBullet : MonoBehaviour
{
    public Transform t;

    private void Start()
    {
        t = GetComponentInParent<Transform>();
    }

    private void OnEnable()
    {
        MultiplayerBulletSystem.components.Add(this);
    }

    private void OnDisable()
    {
        MultiplayerBulletSystem.components.Remove(this);
    }
}