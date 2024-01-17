using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIHealth : MonoBehaviour
{
    public float blinkIntensity;
    public float blinkDuration;
    public float lowHealth = 50;
    [HideInInspector]public float blinkTimer;
    [HideInInspector]public float currenthealth;
    AiAgent_config config;
    public SkinnedMeshRenderer[] meshRenderer;
    Animator anim;
    private List<Material> materials;
    public AudioClip lowHealthSound;
    public AudioSource audioSource;
    public CharacterConfig characterConfig;

    void Awake()
    {
        anim = GetComponent<Animator>();
        config = new AiAgent_config();
        currenthealth = characterConfig.Health;
        materials = new List<Material>();
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in meshRenderer)
        {
            materials.AddRange(skinnedMeshRenderer.materials);
        }
        AddHitbox();
        OnStart();
    }

    private void AddHitbox()
    {
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody1 in rigidbodies)
        {
            HitBox hitbox = rigidbody1.gameObject.AddComponent<HitBox>();
            hitbox.health = this;
            if (hitbox.gameObject != gameObject)
            {
                hitbox.gameObject.layer = 9;
            }
        }
    }
    public bool Isdead()
    {
        return currenthealth <= 0;
    }
    internal void Heal(float amount)
    {
        currenthealth += amount;
        currenthealth = Mathf.Min(currenthealth, characterConfig.Health);
        OnHeal(amount);
    }
    public void AiTakeDamge(float amount, Vector3 direction)
    {
        currenthealth -= amount;
        OnDamage(direction);
        if(currenthealth <= 0)
        {
            Die(direction);
        }
        blinkTimer = blinkDuration;

    }
    public bool IslowHealth()
    {
        return currenthealth < lowHealth;
    }
    private void Die(Vector3 direction)
    {
        OnDead(direction);
    }
    private void Update()
    {
        Blink();
    }

    private void Blink()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        foreach (Material material in materials)
        {
            material.color = Color.white * intensity;
        }
    }

    [ContextMenu("Auto find mess")]
    private void AutoFind()
    {
        meshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    protected virtual void OnStart()
    {

    }
    protected virtual void OnDead(Vector3 direction)
    {

    }
    protected virtual void OnDamage(Vector3 direction)
    {

    }
    protected virtual void OnHeal(float amount)
    {

    }

}
