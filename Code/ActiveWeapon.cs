using System.Collections;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary =0, Secondary =1, Tertiary = 2, Quaternary = 3
    }
    [SerializeField] public bool IsHolster = false;
    public string overrided_anim = "Weapon_Empty";
    public string Equip_Weapon = "Equip_"; // Use In The Character Animatior (Equip_Rife,...)
    public string Holster_WeaponKey = "x";
    public string Holster_AnimatedBool_Name = "Holster_Weapon";
    public string Sprinting_AnimatedWeapon_Index = "Weapon_Index";
    public string NotSprinting_Animation_Name = "NotSprinting";
    public bool IsChangingWeapon = false;
    public Transform CrossTarget;
    public UnityEngine.Animations.Rigging.Rig HandRig;
    public CharactorAimAt CharacterAiming;
    public Transform[] Weapon_Slots;
    public Transform Left_Girm;
    public Transform Right_Girm;
    public Animator Rig_Controller;
    public Ammo_Widget ammo_Widget;
    
    RaycastWeapon[] Equipped_weapons = new RaycastWeapon[4];
    Reload_Weapon reload_Weapon;
    CharactorAimAt aimAt;

    int activeWeapon_Index;
    int NotSprinting_Animation_Hash;// = Animator.StringToHash("NotSprinting");
    int Holster_AnimatedBool_Hash;
    int Sprinting_AnimatedWeapon_Index_Hash;

    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    WaitForSeconds wait = new WaitForSeconds(0.1f);
    Coroutine coroutine;

    RaycastWeapon GetWeapon(int index)
    {
        if (index < 0 || index >= Equipped_weapons.Length)
        { return null; }
        return Equipped_weapons[index];
    }

    private void Awake()
    {
        Holster_AnimatedBool_Hash = Animator.StringToHash(Holster_AnimatedBool_Name);
        NotSprinting_Animation_Hash = Animator.StringToHash(NotSprinting_Animation_Name);
        Sprinting_AnimatedWeapon_Index_Hash = Animator.StringToHash(Sprinting_AnimatedWeapon_Index);
        RaycastWeapon ExitingWeapon = GetComponentInChildren<RaycastWeapon>();
        reload_Weapon = GetComponent<Reload_Weapon>();
        if (ExitingWeapon) { Equip(ExitingWeapon); }
        aimAt = GetComponent<CharactorAimAt>();
    }
    public RaycastWeapon GetRaycastWeapon()
    {
        return GetWeapon(activeWeapon_Index);
    }

    void Update()
    {
        Firing();
        Holster_Weapon();
        Equip_WeaponKey();
    }
    private void Equip_WeaponKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { SetActiveWeapon(WeaponSlot.Primary);   }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { SetActiveWeapon(WeaponSlot.Secondary); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { SetActiveWeapon(WeaponSlot.Tertiary);  }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { SetActiveWeapon(WeaponSlot.Quaternary);}
    }
    public bool IsFiring()
    {
        RaycastWeapon raycastWeapon = GetActiveWeapon();
        if (!raycastWeapon)
        {
            return false;
        }
        return raycastWeapon.IsFiring;
    }
    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeapon(activeWeapon_Index);
    }
    private void Firing()
    {
        bool isReloading = reload_Weapon.IsReloading;
        var weapon = GetWeapon(activeWeapon_Index);
        bool NotSprinting = Rig_Controller.GetCurrentAnimatorStateInfo(2).shortNameHash == NotSprinting_Animation_Hash;
        bool canFire = !IsHolster && NotSprinting && !isReloading;
        if (weapon)
        {
            if (Input.GetButton("Fire1") && canFire && !weapon.IsFiring)
            {
                weapon.StartFiring();
            }
            if (Input.GetButtonUp("Fire1")|| !canFire)
            {
                weapon.StopFiring();
            }
            weapon.UpdateWeapon(Time.deltaTime, CrossTarget.position);
        }
    }
    public void Holster_Weapon()
    {
        var weapon = GetWeapon(activeWeapon_Index);
        if (weapon)
        {
            if (Input.GetKeyDown(Holster_WeaponKey))
            {
                
                TotalActive_weapon();
            }
        }
    }
    public void Equip(RaycastWeapon NewWeapon)
    {
        int WeaponSlotIndex = (int)NewWeapon.WeaponSlot;
        var weapon = GetWeapon(WeaponSlotIndex);
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }
        weapon = NewWeapon;
        weapon._Recoil.CharacterAimingAt = CharacterAiming;
        weapon._Recoil.Rig_Controller = Rig_Controller;
        weapon.transform.SetParent (Weapon_Slots[WeaponSlotIndex], false);
        aimAt.Aim();
        Equipped_weapons[WeaponSlotIndex] = weapon;
        SetActiveWeapon(NewWeapon.WeaponSlot);
        ammo_Widget.Refresh(weapon.ammoCount);
    }
    void TotalActive_weapon()
    {
        bool isHolstered = Rig_Controller.GetBool(Holster_AnimatedBool_Name);
        if (isHolstered)
        {
            StartCoroutine(Active_Weapon(activeWeapon_Index));
        }
        else
        {
            StartCoroutine(HolsterWeapon(activeWeapon_Index));
        }
    }
    void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holster_index = activeWeapon_Index;
        int active_index = (int)weaponSlot;
        if(holster_index == active_index) { holster_index = -1; }
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(SwitchWeapon (holster_index, active_index));
    }
    IEnumerator SwitchWeapon(int holster_index , int Active_index)
    {
        Rig_Controller.SetInteger(Sprinting_AnimatedWeapon_Index_Hash, activeWeapon_Index);
        yield return HolsterWeapon(holster_index);
        yield return Active_Weapon(Active_index);
        activeWeapon_Index = Active_index;
    }
    IEnumerator HolsterWeapon(int index)
    {
        IsChangingWeapon = true;
        IsHolster = true;
        var weapon = GetWeapon(index);
        if(weapon)
        {
            Rig_Controller.SetBool(Holster_AnimatedBool_Hash, true);
            yield return wait;
            do
            {
                yield return waitForEndOfFrame;
            }
            while (Rig_Controller.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
        IsChangingWeapon = false;
    }
    IEnumerator Active_Weapon(int index)
    {
        IsChangingWeapon = true;
        var weapon = GetWeapon(index);
        if (weapon)
        {
            Rig_Controller.SetBool(Holster_AnimatedBool_Hash, false);
            Rig_Controller.Play(Equip_Weapon + weapon.Weapon_Name);
            do
            {
                yield return waitForEndOfFrame;
            }
            while (Rig_Controller.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            IsHolster = false;
        }
        IsChangingWeapon = false;
    }
    public void DropWeapon()
    {
        var currentWeapon = GetActiveWeapon();
        if (currentWeapon)
        {
            currentWeapon.transform.SetParent(null);
            currentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.gameObject.AddComponent<Rigidbody>();
            Equipped_weapons[activeWeapon_Index] = null;
        }
    }
 
}
