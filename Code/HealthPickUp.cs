using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float amount = 50;

    private void OnTriggerEnter(Collider other)
    {
        AIHealth health = other.GetComponent<AIHealth>();
        if(health)
        {
            health.Heal(amount);
            Destroy(gameObject);
            
        }
    }
}
