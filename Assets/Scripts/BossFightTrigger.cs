using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BossFightTrigger : MonoBehaviour
{
    public event Action OnPlayerEnterBossFight;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("Passou pela porta");

            OnPlayerEnterBossFight?.Invoke();
        }
    }
}
