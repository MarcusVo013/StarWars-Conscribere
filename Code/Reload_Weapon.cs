using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Animations;
using UnityEngine;

public class Reload_Weapon : MonoBehaviour
{
    public Weapon_AnimationEvent Weapon_AnimationEvent;
    public Animator Rig_Controler;
    public ActiveWeapon activeWeapon;
    public Transform Left_Hand;
    public Ammo_Widget ammo_Widget;
    public AudioClip reloadSound;
    public AudioSource audioSource;
    GameObject Megazine_LeftHand;
    [SerializeField] public bool IsReloading;
    [SerializeField] string Reload_Key = "r";
    [SerializeField] string Reload_Animation_Name = "Reload_Weapon";
    [SerializeField] float magazineDestroyTime = 2.0f;
    private void Awake()
    {
        activeWeapon = GetComponent<ActiveWeapon>();
    }
    void Start()
    {
        Weapon_AnimationEvent.Weapon_animationEvent.AddListener(OnAnimayionEvent);
    }

    // Update is called once per frame
    void Update()
    {
        bool IsFiring = activeWeapon.IsFiring();
        RaycastWeapon weapon = activeWeapon.GetRaycastWeapon();
        if (weapon)
        {
            if (Input.GetKeyDown(Reload_Key) || weapon.ammoCount <= 0 && !IsFiring)
            {
                audioSource.PlayOneShot(reloadSound);
                IsReloading = true;
                Rig_Controler.SetTrigger(Reload_Animation_Name);
            }
            if(weapon.IsFiring)
            {
                ammo_Widget.Refresh(weapon.ammoCount);
            }

        }
        
    }
    void OnAnimayionEvent(string EventName) {
    Debug.Log(EventName);
        switch (EventName)
        {
            case "detach_megazine":Detach_Megazine(); break;
            case "drop_megazine":Drop_Megazine(); break;
            case "get_megazine":Get_Megazine(); break;
            case "attach_megazine":Attach_Megazine(); break;
        }
    }
    void Detach_Megazine()
    {
        RaycastWeapon weapon = activeWeapon.GetRaycastWeapon();
        Megazine_LeftHand = Instantiate(weapon.Full_mag, Left_Hand, true);
        weapon.Full_mag.SetActive(false);
    }
    void Drop_Megazine()
    {
        GameObject droppedMagazine = Instantiate(Megazine_LeftHand, Megazine_LeftHand.transform.position, Megazine_LeftHand.transform.rotation);
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();
        Megazine_LeftHand.SetActive(false);
        Destroy(droppedMagazine, magazineDestroyTime);
    }


    void Get_Megazine()
    {
        Megazine_LeftHand.SetActive(false);
    }
    void Attach_Megazine()
    {
        RaycastWeapon weapon = activeWeapon.GetRaycastWeapon();
        weapon.Full_mag.SetActive(true);
        Destroy(Megazine_LeftHand);
        weapon.ammoCount = weapon.ClipSize;
        Rig_Controler.ResetTrigger(Reload_Animation_Name);
        ammo_Widget.Refresh(weapon.ammoCount);
        IsReloading = false;
    }

}
