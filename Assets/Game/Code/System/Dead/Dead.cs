using System.Collections.Generic;
using UnityEngine;

// #jam
public class Dead : MonoBehaviour
{
    public List<Stats> deads = new List<Stats>();

    [Header("By GetComponent")]
    public Stats stats;
    public Damage damage;

    private void Start()
    {
        stats = GetComponent<Stats>();
        damage = GetComponent<Damage>();
        damage.OnDamage += stats => deads.Add(stats);
    }

    private void OnEnable()
    {
        DeadSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        DeadSystem.entities.Remove(this);
    }
}