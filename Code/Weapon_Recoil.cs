using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Recoil : MonoBehaviour
{
    [HideInInspector] public CharactorAimAt CharacterAimingAt;
    [HideInInspector] public Cinemachine.CinemachineImpulseSource Camera_Shake;
    [HideInInspector] public Animator Rig_Controller;
    [HideInInspector] float Time_Recoil;
    [HideInInspector] int index;
    [HideInInspector] float Vertical_Recoil;
    [HideInInspector] float Horizontal_Recoil;
    [SerializeField] Vector2[] Recoil_Pattern;
    [SerializeField] float Duration;
    [SerializeField] string Recoil_Animation = "Weapon_Recoil_";

    public void Reset()
    {
        index = 0;
    }
    int NextIndex(int index)
    {
        return (index +1 ) % Recoil_Pattern.Length;
    }
    private void Awake()
    {
        Camera_Shake = GetComponent<CinemachineImpulseSource>();       
    }
    public void Genarate_Recoil(string WeaponName)
    {
        Time_Recoil = Duration;
        Camera_Shake.GenerateImpulse(Camera.main.transform.forward);
        Horizontal_Recoil = Recoil_Pattern[index].x;
        Vertical_Recoil = Recoil_Pattern[index].y;
        index = NextIndex(index);
        Rig_Controller?.Play(Recoil_Animation+WeaponName, 1,0.0f);
    }
    void Update()
    {
        if(Time_Recoil > 0)
        {
            CharacterAimingAt.yAxis.Value -= ((Vertical_Recoil / 10) * Time.deltaTime) / Duration;
            CharacterAimingAt.xAxis.Value -= ((Horizontal_Recoil / 10) * Time.deltaTime) / Duration;
            Time_Recoil -= Time.deltaTime;
        }
        
    }
}
