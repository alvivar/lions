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

    [ContextMenu("RandomPosition")]
    public void RandomPosition()
    {
        var x = Random.Range(1000, 9999);
        var y = Random.Range(1000, 9999);
        var z = Random.Range(1000, 9999);
        transform.position = new Vector3(x, y, z);
    }
}