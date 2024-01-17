using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaysCastWeapon : MonoBehaviour
{
    class bullet
    {
        public float Time;
        public Vector3 InitialPosition;
        public Vector3 InitialVelocity;
        public TrailRenderer Tracer;
        public int bounce;
    }

    public int ammoCount;
    public int ClipSize;
    public float Weapon_Damage = 10;
    public float Melee_Damage;
    public LayerMask layerMask;
    [SerializeField] int FireRate = 30;
    [SerializeField] float BulletSpeed = 1000f;
    [SerializeField] float BulletDrop = 0f;
    [SerializeField] float BulletLifeTime = 3;
    [SerializeField] int MaxBounces = 0;
    [SerializeField] public bool IsFiring = false;
    [SerializeField] ParticleSystem[] MuzzleFlash;
    [SerializeField] ParticleSystem HitEffect;
    [SerializeField] Transform RaycastOrigin;
    [SerializeField] TrailRenderer TracerEffect;

    Ray Ray;
    HitBox HitBox;
    RaycastHit HitInfo;
    List<bullet> Bullets = new List<bullet>();

    public AudioClip fireSound;
    public AudioSource audioSource;
    private float AccumilatedTime;
    public Transform RaycastDestiation;
    public string Weapon_Name;
    public ActiveWeapon.WeaponSlot WeaponSlot;
    public GameObject Full_mag;
    Vector3 GetPosition(bullet bullet)
    {
        Vector3 gravity = Vector3.down * BulletDrop;
        return (bullet.InitialPosition) + (bullet.InitialVelocity * bullet.Time) + (0.5f * gravity * bullet.Time * bullet.Time);
    }

    bullet CreateBullet(Vector3 Position, Vector3 velocity)
    {
        bullet Bullet = new()
        {
            InitialPosition = Position,
            InitialVelocity = velocity,
            Time = 0.0f,
            bounce = MaxBounces,
            Tracer = Instantiate(TracerEffect, Position, Quaternion.identity)
        };
        Bullet.Tracer.AddPosition(Position);
        return Bullet;
    }

    public void StartFiring()
    {
        if (AccumilatedTime > 0.0f)
        {
            AccumilatedTime = 0.0f;
        }
        IsFiring = true;
    }
    public void UpdateWeapon(float deltaTime, Vector3 target)
    {
        if (IsFiring)
        {
            UpdateFiring(deltaTime, target);
        }
        AccumilatedTime += deltaTime;
        UpdateBullet(deltaTime);
    }
    public void UpdateFiring(float Deltatime, Vector3 target)
    {
        float FireInterval = 1.0f / FireRate;
        while (AccumilatedTime >= 0.0f)
        {
            FireBullet(target);
            AccumilatedTime -= FireInterval;
        }
    }
    public void UpdateBullet(float DealtaTime)
    {
        SimulateBullet(DealtaTime);
        DestroyBullet();
    }
    private void SimulateBullet(float Deltatime)
    {
        foreach (var bullet in Bullets)
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.Time += Deltatime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        }
    }

    private void RaycastSegment(Vector3 start, Vector3 end, bullet Bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        Ray.origin = start;
        Ray.direction = direction;
        if (Physics.Raycast(Ray, out HitInfo, distance, layerMask))
        {
            HitEffect.transform.position = HitInfo.point;
            HitEffect.transform.forward = HitInfo.normal;
            HitEffect.Emit(1);
            Bullet.Tracer.transform.position = HitInfo.point;
            Bullet.Time = BulletLifeTime;

            //Collision Impulse.
            HitBox = HitInfo.collider.GetComponent<HitBox>();
            if (HitBox)
            {
                HitBox.EnemyOnRaycastHit(this, Ray.direction);
            }

            //Add Bounce TO Bullet
            if (Bullet.bounce > 0)
            {
                Bullet.Time = 0;
                Bullet.InitialPosition = HitInfo.point;
                Bullet.InitialVelocity = Vector3.Reflect(Bullet.InitialVelocity, HitInfo.normal);
                Bullet.bounce--;
            }
            //Add Force To Bullet
            var rb2d = HitInfo.collider.GetComponent<Rigidbody>();
            if (rb2d)
            {
                rb2d.AddForceAtPosition(Ray.direction * 20, HitInfo.point, ForceMode.Impulse);
            }
        }
        else
        {
            Bullet.Tracer.transform.position = end;
        }
    }
    private void DestroyBullet()
    {
        for (int i = Bullets.Count - 1; i >= 0; i--)
        {
            bullet bullet = Bullets[i];
            if (bullet.Time >= BulletLifeTime)
            {
                Bullets.RemoveAt(i);
                Destroy(bullet.Tracer.gameObject, 1f);
            }
        }
    }
    private void FireBullet(Vector3 target) // Bullet With Physic
    {
        if (ammoCount <= 0) { return; }
        ammoCount--;
        foreach (var Particle in MuzzleFlash)
        {
            Particle.Emit(1);
            audioSource.PlayOneShot(fireSound);
        }

        Vector3 velocity = (target - RaycastOrigin.position).normalized * BulletSpeed;
        var Bullet = CreateBullet(RaycastOrigin.position, velocity);
        Bullets.Add(Bullet);
        
    }

    public void StopFiring()
    {
        IsFiring = false;
        foreach (var Particle in MuzzleFlash)
        {
            Particle.Emit(0);
        }
    }
}
