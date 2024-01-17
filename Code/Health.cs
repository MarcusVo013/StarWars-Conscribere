using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{


    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;
    float currenthealth;
    float rebornHealth;
    public SkinnedMeshRenderer[] meshRenderer;
    public ZombieOpiton zombieOpiton;
    public Animator animator;
    RagDoll ragDoll;
    public float dieForce =2;
    private List<Material> materials;
    public int rebornChangeNumber =50;
    bool isReborn =false;
    [Header("___Audio___")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip deadSound;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] AudioClip rebornSound;
    void Awake()
    {
        zombieOpiton = GetComponent<ZombieOpiton>();
        ragDoll = GetComponent<RagDoll>();
        currenthealth = zombieOpiton.enemyConfig.Health;
        
        materials = new List<Material>();
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in meshRenderer)
        {
            materials.AddRange(skinnedMeshRenderer.materials);
        }
        AddHitBox();
    }
    private void AddHitBox()
    {
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody1 in rigidbodies)
        {
            ZombieHitBox hitbox = rigidbody1.gameObject.AddComponent<ZombieHitBox>();
            hitbox.health1 = this;
            if (hitbox.gameObject != gameObject)
            {
                hitbox.gameObject.layer = 9;
            }
        }
    }
    public void TakeDamge(float amount, Vector3 direction)
    {
        currenthealth -= amount;
        rebornHealth -= amount;
        Debug.Log(rebornHealth);
        if (currenthealth <= 0 )
        {
            if (!isReborn)
            {
             RebornChange(direction);
            }
            if (rebornHealth <= 0)
            {
                Debug.Log("Ok cant die");
                RealDeath(direction);
            }
        }
        blinkTimer = blinkDuration;
        zombieOpiton.OnPlayerAttack();
    }
    private void RebornChange(Vector3 direction)
    {
        int change = UnityEngine.Random.Range(0, 50);
        if (change <= rebornChangeNumber) 
        {
            rebornHealth = zombieOpiton.enemyConfig.rebornHealth;
            ragDoll.RagdollOff();
            animator.SetTrigger("Reborn");
            audioSource.PlayOneShot(rebornSound);
            isReborn = true;
        }
        else
        {
            RealDeath(direction);
        }
    }
    private void RealDeath(Vector3 direction)
    {
        ragDoll.RagdollOn();
        direction.y = 1;
        ragDoll.ApplyForce(direction * dieForce);
        Destroy(this.gameObject, zombieOpiton.enemyConfig.destroyTime);
        audioSource.PlayOneShot(deadSound);
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
    
}
