using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : AIHealth
{
    RagDoll ragDoll;
    ActiveWeapon activeWeapon;
    CharactorAimAt aimAt;
    VolumeProfile postProcessing;
    CameraManager cameraManager;
    PlayerData playerData;
    bool Islowhealth = false;
    bool Isdead= false;
    bool isCheat = false;
    [SerializeField]GameObject gameover;
    [Header("__Sound Option__")]
    public AudioClip HealthSound;
    public AudioClip hurtSound;
    public AudioClip deadSound;
    [Header("__Camera Shake Option__")]
    public CameraShake cameraShake;
    public float intensity =2;
    public float shakeTime = 1;

    protected override void OnStart()
    {
        ragDoll = GetComponent<RagDoll>();
        activeWeapon = GetComponent<ActiveWeapon>();
        aimAt = GetComponent<CharactorAimAt>();
        postProcessing = FindObjectOfType<Volume>().profile;
        cameraManager = FindObjectOfType<CameraManager>();
    }
    protected override void OnDead(Vector3 direction)
    {
        direction.y = 1.0f;
        ragDoll.RagdollOn();
        ragDoll.ApplyForce(direction);
        activeWeapon.DropWeapon();
        aimAt.enabled = false;
        cameraManager.KillCamOn();
        Invoke("Isgameover", 1f);
        Isdead = true;
        audioSource.PlayOneShot(deadSound);
    }
    protected override void OnDamage(Vector3 direction)
    {
        UpdateVignette();
        if (!Isdead)
        {
            audioSource.PlayOneShot(hurtSound);
        }
        if (currenthealth < lowHealth && !Islowhealth)
        {
            audioSource.PlayOneShot(lowHealthSound);
            Islowhealth = true;
        }
        cameraShake.ShakeCamera(intensity,shakeTime);
    }
    protected override void OnHeal(float amount)
    {
        UpdateVignette();
        audioSource.PlayOneShot(HealthSound);

    }
    private void Isgameover()
    {
        gameover.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    private void UpdateVignette()
    {
        Vignette vignette;
        if (postProcessing.TryGet(out vignette))
        {
            float precent = 1.0f - (currenthealth / characterConfig.Health);
            vignette.intensity.value = precent * 0.5f;
        }
    }
    public void Cheat()
    {
        isCheat = true;
        if (isCheat)
        {
            currenthealth = 99999999;
            Debug.Log(currenthealth);
        }
    }

}
