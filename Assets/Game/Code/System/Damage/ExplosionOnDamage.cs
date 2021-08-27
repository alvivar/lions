using System.Collections.Generic;
using UnityEngine;

// #jam
public class ExplosionOnDamage : MonoBehaviour
{
    public List<Stats> explosions = new List<Stats>();

    [Header("By GetComponent")]
    public Renderer render;
    public Damage damage;

    private void Start()
    {
        render = GetComponent<Renderer>();
        damage = GetComponent<Damage>();
        damage.OnDamage += stats => explosions.Add(stats);
    }

    private void OnEnable()
    {
        ExplosionOnDamageSystem.components.Add(this);
    }

    private void OnDisable()
    {
        ExplosionOnDamageSystem.components.Remove(this);
    }
}