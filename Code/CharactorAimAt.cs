using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class CharactorAimAt : MonoBehaviour
{
    [SerializeField] float aimTime =0.2f ;
    [SerializeField] float turnspeed = 15f;
    [SerializeField] Camera MainCamera;
    [SerializeField] GameObject AimCross;
    [SerializeField] Transform CamLookAt;
    
    Animator animator;

    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;
    int isAimingParam = Animator.StringToHash("IsAiming");
    public bool isInteract = false;

    private void Awake()
    {
        MainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isInteract)
        {
            bool isAiming = Input.GetMouseButton(1);
            animator.SetBool(isAimingParam, isAiming);
        }
    }

    void FixedUpdate()
    {
        if (!isInteract)
        {
            xAxis.Update(Time.deltaTime);
            yAxis.Update(Time.deltaTime);
            CamLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
            InputYcamera();
        }
    }
    public void Aim()
    {       
        AimCross.SetActive(true);
    }
    public void AimOff()
    {
        AimCross.SetActive(false);
    }

    private void InputYcamera()
    {
        float YawCam = MainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.Euler(0,YawCam,0), turnspeed*Time.fixedDeltaTime);
    }
}
