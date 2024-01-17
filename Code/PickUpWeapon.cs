using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public RaycastWeapon WeaponPrefab;
    public EnemyRaysCastWeapon enemyWeaponPreFab;


    private void OnTriggerEnter(Collider WeaponBall)
    {
        ActiveWeapon activeWeapon = WeaponBall.gameObject.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {
            RaycastWeapon newWeapon = Instantiate(WeaponPrefab);
            activeWeapon.Equip(newWeapon);
            Destroy(gameObject);
        }
        AiWeapon AiWeapon = WeaponBall.gameObject.GetComponent<AiWeapon>();
        if (AiWeapon)
        {
            EnemyRaysCastWeapon newWeapon = Instantiate(enemyWeaponPreFab);
            AiWeapon.EquipWeapon(newWeapon);
            Destroy(gameObject);
        }

    }

}
