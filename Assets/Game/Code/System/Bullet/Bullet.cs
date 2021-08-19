using UnityEngine;

// #jam
public class Bullet : MonoBehaviour
{
    [Header("By GetComponent")]
    public Stats stats;
    public Rigidbody rbody;

    private void Start()
    {
        stats = GetComponent<Stats>();
        rbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        BulletSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        BulletSystem.entities.Remove(this);
    }
}