using UnityEngine;

// #jam
public class RotateByStatsDir : MonoBehaviour
{
    public Vector3 lastDir;

    [Header("Required")]
    public Transform target;

    [Header("By GetComponent")]
    public Stats stats;

    void Start()
    {
        stats = GetComponent<Stats>();
    }

    void OnEnable()
    {
        RotateByStatsDirSystem.components.Add(this);
    }

    void OnDisable()
    {
        RotateByStatsDirSystem.components.Remove(this);
    }
}