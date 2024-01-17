using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleAttack : MonoBehaviour
{
    public EnemyConfig enemyConfig;
    public ZombieOpiton zombieOpiton;
    public Vector3 direction;
    public GameObject hand;
    private void Awake()
    {
        direction = hand.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        AIHealth damaged = other.GetComponent<AIHealth>();
        if (damaged)
        {
            damaged.AiTakeDamge(enemyConfig.atkDamage, direction);
            zombieOpiton.audioSource.PlayOneShot(zombieOpiton.atkSound);
        }
    }
}
