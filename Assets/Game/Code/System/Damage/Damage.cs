using System;
using System.Collections.Generic;
using UnityEngine;

// #jam
public class Damage : MonoBehaviour
{
    public Action<Stats> OnDamage;
    public List<Stats> collisions = new List<Stats>();

    [Header("By GetComponent")]
    public Stats stats;

    private void Start()
    {
        stats = GetComponent<Stats>();
    }

    private void OnEnable()
    {
        DamageSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        DamageSystem.entities.Remove(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var stats = other.gameObject.GetComponent<Stats>();
        if (!stats)
            return;

        collisions.Add(stats);
    }
}