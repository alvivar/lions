using System.Collections.Generic;
using UnityEngine;

// #jam
public class HideOnDamage : MonoBehaviour
{
    public List<Damage> damages = new List<Damage>();

    [Header("By GetComponent")]
    public Stats stats;
    public Damage damage;
    public Bullet bullet;

    private void Start()
    {
        stats = GetComponent<Stats>();
        damage = GetComponent<Damage>();
        damage.OnDamage += stats => damages.Add(damage);
        bullet = GetComponent<Bullet>();
    }

    private void OnEnable()
    {
        HideOnDamageSystem.components.Add(this);
    }

    private void OnDisable()
    {
        HideOnDamageSystem.components.Remove(this);
    }
}