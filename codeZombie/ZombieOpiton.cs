using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieOpiton : MonoBehaviour
{
    public EnemyConfig enemyConfig;
    public Collider rightHand;

    public bool playerAttacked;
    Transform player;
    [Header("___Audio___")]
    public AudioSource audioSource;
    public AudioClip atkSound;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void Attacking()
    {
        rightHand.enabled = true;
    }
    public void NotAttacking()
    {
        rightHand.enabled = false;
    }
    public void OnPlayerAttack()
    {
        playerAttacked = true;
    }
    public void UpdateTargetPlayerPosition()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject zombie in zombies)
        {
            zombie.GetComponent<ZombieOpiton>().player = player;
        }
    }
}
