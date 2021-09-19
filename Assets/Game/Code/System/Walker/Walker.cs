using UnityEngine;

// #jam
public class Walker : MonoBehaviour
{
    public float zLayer = 0;

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
        WalkerSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        WalkerSystem.entities.Remove(this);
    }
}