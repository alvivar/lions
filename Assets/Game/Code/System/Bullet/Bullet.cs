using UnityEngine;

// #jam
public class Bullet : MonoBehaviour
{
    [Header("By GetComponent")]
    public Stats stats;
    public Rigidbody2D rbody;
    public TrailRenderer trail;

    private void Start()
    {
        stats = GetComponent<Stats>();
        rbody = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        BulletSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        BulletSystem.entities.Remove(this);
    }

    public void Fire(Vector3 pos, Vector3 dir)
    {
        trail.Clear();
        trail.enabled = true;

        transform.position = pos;

        stats.direction = dir;
        stats.duration = 0;
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