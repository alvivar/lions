using UnityEngine;

// #jam
public class PuppetRotation : MonoBehaviour
{
    public Transform t;

    private void Start()
    {
        t = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        PuppetRotationSystem.components.Add(this);
    }

    private void OnDisable()
    {
        PuppetRotationSystem.components.Remove(this);
    }
}