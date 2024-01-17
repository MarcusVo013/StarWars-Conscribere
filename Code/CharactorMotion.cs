using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorMotion : MonoBehaviour
{
    [SerializeField] Animator Rig_Controller;
    [SerializeField] float JumpHight;
    [SerializeField] float Gravity;
    [SerializeField] float StepOffset;
    [SerializeField] float Air_Controller;
    [SerializeField] float Damp;
    [SerializeField] string JumpAnim_Name = "IsJumping";
    [SerializeField] float pushPower;

    public CharacterConfig characterConfig;
    Animator anim;
    ActiveWeapon activeWeapon;
    CharacterController cc;
    Reload_Weapon reloadWeapon;
    Vector2 input;
    Vector3 rootMotion;
    Vector3 velocity;
    bool isJumping;
    public bool isInteract =false;
    readonly int isSprinting_Hash = Animator.StringToHash("IsSprinting");
    private void Awake()
    {
        anim = GetComponent<Animator>(); 
        cc = GetComponent<CharacterController>();
        activeWeapon = GetComponent<ActiveWeapon>();
        reloadWeapon = GetComponent<Reload_Weapon>();
    }
   
    void Update()
    {
        if (!isInteract)
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            anim.SetFloat("InputX", input.x);
            anim.SetFloat("InputY", input.y);
            if (Input.GetKeyDown(KeyCode.Space))
            { Jump(); }
        }
    }
    private void OnAnimatorMove()
    {
        rootMotion += anim.deltaPosition;
    }
    private void FixedUpdate()
    {
        if (!isInteract)
        {
            OnAirAndGrounded();
            UpdateIsSprinting();
        }
    }
   
    void OnAirAndGrounded()
    {
        if(isJumping)
        {
            UpdateInAir();
        }
        else
        {
            UpdateOnGround();
        }      
    }
    bool IsSprinting()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        bool isFiring = activeWeapon.IsFiring();
        bool isReloading = reloadWeapon.IsReloading;
        bool isChangingWeapon = activeWeapon.IsChangingWeapon;
        return (!isFiring && isSprinting && !isReloading && !isChangingWeapon);
    }
    private void UpdateIsSprinting()
    {
        bool isSprinting = IsSprinting();
        anim.SetBool(isSprinting_Hash, isSprinting);
        Rig_Controller.SetBool(isSprinting_Hash, isSprinting);
    }

    private void UpdateInAir()
    {
        velocity.y -= Gravity * Time.fixedDeltaTime;
        Vector3 displace = velocity * Time.fixedDeltaTime;
        displace += CalculateAir_Controller();
        cc.Move(displace);
        isJumping = !cc.isGrounded;
        rootMotion = Vector3.zero;
        anim.SetBool(JumpAnim_Name, isJumping);
    }

    private void UpdateOnGround()
    {
        Vector3 StepDownAmount = Vector3.down * StepOffset;
        Vector3 StepForwardAmount = rootMotion * characterConfig.GroundSpeed;
        cc.Move(StepForwardAmount + StepDownAmount);
        rootMotion = Vector3.zero;
        if (!cc.isGrounded)
        {
            SetInAir(0);
        }
    }

    void Jump()
    {
        if(!isJumping)
        {
            float JumpVelocity = Mathf.Sqrt(2 * Gravity * JumpHight);
            SetInAir(JumpVelocity);
        }
    }

    private void SetInAir(float JumpVelocity)
    {
        isJumping = true;
        velocity = anim.velocity * Damp * characterConfig.GroundSpeed;
        velocity.y = JumpVelocity;
        anim.SetBool(JumpAnim_Name, true);
    }
    Vector3 CalculateAir_Controller()
    {
        return (((transform.forward * input.y) + (transform.right * input.x)) * (Air_Controller / 100)); 
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;
        if (hit.moveDirection.y < -0.3f)
            return;
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }
}
