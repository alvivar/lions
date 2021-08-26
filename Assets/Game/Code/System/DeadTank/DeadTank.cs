using System.Collections.Generic;
using UnityEngine;

// #jam
public class DeadTank : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();

    [Header("By GetComponent")]
    public Rigidbody2D[] rigidBodies;

    private void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody2D>();
    }

    private void OnEnable()
    {
        DeadTankSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        DeadTankSystem.entities.Remove(this);
    }

    public void SetPosition(Vector3 pos)
    {
        positions.Add(pos);
    }
}