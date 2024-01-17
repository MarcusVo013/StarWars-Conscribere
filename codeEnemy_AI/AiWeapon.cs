using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeapon : MonoBehaviour
{
    public float inaccuracy = 0.0f;
    public EnemyRaysCastWeapon currentWeapon;
    Animator animator;
    MeshSockets sockets;
    WeaponIK weaponIK;
    Transform currentTarget;
    bool weaponActive = false;
    public void Start()
    {
        animator = GetComponent<Animator>();
        sockets = GetComponent<MeshSockets>();
        weaponIK = GetComponent<WeaponIK>();
    }
    public void Update()
    {
        if (currentTarget)
        {
            Vector3 target = currentTarget.position + weaponIK.targetOffSet;
            target += Random.insideUnitSphere * inaccuracy;
            currentWeapon.UpdateWeapon(Time.deltaTime, target);
        }
    }
    public void SetFiring(bool enabled)
    {
        if (enabled)
        {
            currentWeapon.StartFiring();
        }
        else { currentWeapon.StopFiring(); }
    }
    public void ActivateWeapon()
    {
        StartCoroutine(EquipWeapon());
    }
    public void DeactivateWeapon()
    {
        SetFiring(false);
        SetTarget(null);
    }
    IEnumerator EquipWeapon()
    {
        yield return weaponIK;       
        weaponIK.SetAimTranform(currentWeapon.RaycastDestiation);
        weaponActive = true;
    }
    public void EquipWeapon(EnemyRaysCastWeapon weapon)
    {
        currentWeapon = weapon;
        currentWeapon.transform.SetParent(transform,false);
        sockets.Attach(weapon.transform, MeshSockets.SocketId.Spine);
    }
    public void SetTarget(Transform target)
    {
        weaponIK.SetTargetTranform(target);
        currentTarget = target;
    }
    public bool HasWeapon()
    {
        return currentWeapon != null;
       
    }
}
