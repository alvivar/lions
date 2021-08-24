using UnityEngine;

// #jam
public class PunchOnDamage : MonoBehaviour
{
    private void OnEnable()
    {
        PunchOnDamageSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        PunchOnDamageSystem.entities.Remove(this);
    }
}