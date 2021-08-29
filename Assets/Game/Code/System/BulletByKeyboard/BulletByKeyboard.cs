using System;
using UnityEngine;

// #jam
public class BulletByKeyboard : MonoBehaviour
{
    public Action<Vector3, Vector3> OnBullet;

    public int spaceDown = 0;
    public float delay = 0;

    [Header("By GetComponent")]
    public KeyboardInput input;

    [Header("Required")]
    public Transform origin;

    private void Start()
    {
        input = GetComponent<KeyboardInput>();
    }

    private void OnEnable()
    {
        BulletByKeyboardSystem.entities.Add(this);
    }

    private void OnDisable()
    {
        BulletByKeyboardSystem.entities.Remove(this);
    }
}