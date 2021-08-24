using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10;
    public float dampening = 5;
    public Vector3 direction;

    [Header("Time")]
    public float duration = 0;

    [Header("Combat")]
    public int health;
    public int damage = 0;
    public float punch = 0;
}