using UnityEngine;

// #jam
public class HumanAnim : MonoBehaviour
{
    public Transform target;

    [Header("By GetComponent")]
    public Stats stats;
    public Snapshot snapshot;

    private void Start()
    {
        stats = GetComponent<Stats>();
        snapshot = GetComponent<Snapshot>();
    }

    private void OnEnable()
    {
        HumanAnimSystem.components.Add(this);
    }

    private void OnDisable()
    {
        HumanAnimSystem.components.Remove(this);
    }
}