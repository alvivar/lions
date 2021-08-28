using UnityEngine;

// #jam
public class AutoAim : MonoBehaviour
{
    public Stats stats;

    private void Start()
    {
        stats = GetComponent<Stats>();
    }

    private void OnEnable()
    {
        AutoAimSystem.aims.Add(this);
    }

    private void OnDisable()
    {
        AutoAimSystem.aims.Remove(this);
    }
}