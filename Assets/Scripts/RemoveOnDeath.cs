using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class RemoveOnDeath : MonoBehaviour
{
    void Start()
    {
        GetComponent<Health>().OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
