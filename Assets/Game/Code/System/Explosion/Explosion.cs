using System.Collections.Generic;
using UnityEngine;

// #jam
public class Explosion : MonoBehaviour
{
    public List<Vector3> explodeAt = new List<Vector3>();

    public Renderer render;

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        ExplosionSystem.components.Add(this);
    }

    private void OnDisable()
    {
        ExplosionSystem.components.Remove(this);
    }

    public void At(Vector3 position)
    {
        explodeAt.Add(position);
    }
}