using UnityEngine;

// #jam
public class Punch : MonoBehaviour
{
    private void OnEnable()
    {
        PunchSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        PunchSystem.entities.Remove(this);
    }
}