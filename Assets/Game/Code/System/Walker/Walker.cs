using UnityEngine;

public class Walker : MonoBehaviour
{
    public Stats stats;

    private void Start()
    {
        stats = GetComponent<Stats>();
    }

    private void OnEnable()
    {
        WalkerSystem.walkers.Add(this);
    }

    private void OnDisable()
    {
        WalkerSystem.walkers.Remove(this);
    }
}