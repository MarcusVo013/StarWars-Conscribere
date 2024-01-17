using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public AIHealth health;
    public void OnRaycastHit(RaycastWeapon raycastWeapon, Vector3 direction)
    {
        health.AiTakeDamge(raycastWeapon.Weapon_Damage, direction);
        
    }
    public void EnemyOnRaycastHit(EnemyRaysCastWeapon enemyraycastWeapon, Vector3 direction)
    {
        health.AiTakeDamge(enemyraycastWeapon.Weapon_Damage, direction);

    }

}
