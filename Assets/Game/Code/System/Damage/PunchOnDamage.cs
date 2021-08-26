using System.Collections.Generic;
using UnityEngine;

// #jam
public class PunchOnDamage : MonoBehaviour
{
    public List<Stats> punches = new List<Stats>();

    [Header("By GetComponent")]
    public Punch punch;
    public Damage damage;

    private void Start()
    {
        punch = GetComponent<Punch>();
        damage = GetComponent<Damage>();
        damage.OnDamage += stat => punches.Add(stat);
    }

    private void OnEnable()
    {
        PunchOnDamageSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        PunchOnDamageSystem.entities.Remove(this);
    }
}