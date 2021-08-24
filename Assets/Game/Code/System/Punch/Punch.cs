using UnityEngine;

// #jam
public class Punch : MonoBehaviour
{
    public bool randomDir = false;
    public Vector2 force;

    [Header("By GetComponent")]
    public Stats stats;
    public Rigidbody2D rbody;
    public Bullet bullet;

    private void Start()
    {
        stats = GetComponent<Stats>();
        rbody = GetComponentInChildren<Rigidbody2D>();
        bullet = GetComponent<Bullet>();
    }

    private void OnEnable()
    {
        PunchSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        PunchSystem.entities.Remove(this);
    }
}