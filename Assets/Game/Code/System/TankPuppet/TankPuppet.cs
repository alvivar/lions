using UnityEngine;

// #jam
public class TankPuppet : MonoBehaviour
{
    public int id = -1;
    public Vector3 serverPosition = Vector3.one * -9999;

    [Header("By GetComponent")]
    public Stats stats;

    private void Start()
    {
        stats = GetComponent<Stats>();
    }

    private void OnEnable()
    {
        TankPuppetSystem.components.Add(this);
    }

    private void OnDisable()
    {
        TankPuppetSystem.components.Remove(this);
    }
}