using UnityEngine;

// #jam
public class Slowdown : MonoBehaviour
{
    [Header("By GetComponent")]
    public Rigidbody2D rigidBody;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        SlowdownSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        SlowdownSystem.entities.Remove(this);
    }
}