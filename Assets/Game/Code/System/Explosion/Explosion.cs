using System.Collections.Generic;
using UnityEngine;

// #jam
public class Explosion : MonoBehaviour
{
    public struct Cmd { public Vector3 position; public float scale; }
    public List<Cmd> explodeAt = new List<Cmd>();

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

    public void At(Vector3 position, float scale)
    {
        explodeAt.Add(new Cmd { position = position, scale = scale });
    }
}