using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitBox : MonoBehaviour
{
    public Health health1;
    public void OnRayCastHit(RaycastWeapon raycastWeapon, Vector3 direction)
    {
        health1.TakeDamge(raycastWeapon.Weapon_Damage, direction);
    }
}
