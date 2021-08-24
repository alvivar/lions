using UnityEngine;

// #jam
public class Punch : MonoBehaviour
{
    public Vector2 force;

    [Header("By GetComponent")]
    public Stats stats;
    public Rigidbody2D rbody;

    private void Start()
    {
        stats = GetComponent<Stats>();
        rbody = GetComponentInChildren<Rigidbody2D>();
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