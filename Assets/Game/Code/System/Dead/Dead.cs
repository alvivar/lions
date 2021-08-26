using System.Collections.Generic;
using UnityEngine;

// #jam
public class Dead : MonoBehaviour
{
    public List<bool> deads = new List<bool>();

    [Header("By GetComponent")]
    public Damage damage;

    private void Start()
    {
        damage = GetComponent<Damage>();
        damage.OnDamage += stat =>
        {
            if (stat.health < 0)
                deads.Add(true);
        };
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