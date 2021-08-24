using UnityEngine;

// #jam
public class Bullet : MonoBehaviour
{
    [Header("By GetComponent")]
    public Stats stats;
    public Rigidbody2D rbody;

    private void Start()
    {
        stats = GetComponent<Stats>();
        rbody = GetComponent<Rigidbody2D>();
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