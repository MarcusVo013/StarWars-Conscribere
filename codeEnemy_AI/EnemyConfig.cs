using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="___State Machine Enemy Script___")]
public class EnemyConfig : ScriptableObject
{
    [Header("___Health Option___")]
    public float Health = 100;
    public float rebornHealth = 100;
    public float destroyTime = 4f;
    [Header("___Chase Option___")]
    public float chaseSpeed = 6;
    public float chaseRange = 10;
    public float stopChasingRange = 20;

    [Header("___Attacking Option___")]
    public float attackRange = 1.3f;
    public float atkDamage = 5;


}
