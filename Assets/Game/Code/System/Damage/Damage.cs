using UnityEngine;

// #jam
public class Damage : MonoBehaviour
{
    private void OnEnable()
    {
        DamageSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        DamageSystem.entities.Remove(this);
    }
}