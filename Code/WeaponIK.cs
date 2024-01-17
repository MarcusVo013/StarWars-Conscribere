using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanBone
{
    public HumanBodyBones bone;
    public float weight0 = 1.0f;
}
public class WeaponIK : MonoBehaviour
{
    public Transform targetTransform;
    public Transform aimTransform;

    public int interation = 10;
    [Range(0, 1)]
    public float weight = 10.0f;

    public float angleLimit = 90.0f;
    public float distanceLimit = 1.5f;
    public Vector3 targetOffSet;

    public HumanBone[] humanBone;
    Transform[] boneTransform;
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        boneTransform = new Transform[humanBone.Length];
        for(int i =0;i < boneTransform.Length; i++)
        {
            boneTransform[i] = animator.GetBoneTransform(humanBone[i].bone);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(aimTransform == null) { return; }
        if (targetTransform == null) { return; }
        Vector3 targetPosition = GetTargetPosition(); 
        for(int i = 0; i < interation; i++) 
        {
            for (int j = 0;j <boneTransform.Length;j++) 
            {
                Transform bone = boneTransform[j];
                float boneWeight = humanBone[j].weight0 * weight;
                AimAtTranform(bone, targetPosition, boneWeight);
            }
        }
    }
    Vector3 GetTargetPosition()
    {
        Vector3 targetDirection = (targetTransform.position + targetOffSet) - aimTransform.position;
        Vector3 aimDirection = aimTransform.forward;
        float blendOut = 0.0f;

        float targetAngle = Vector3.Angle(targetDirection, aimDirection);
        if(targetAngle > angleLimit)
        {
            blendOut += (targetAngle - angleLimit) / 50.0f;
        }
        float targetDistance = targetDirection.magnitude;
        if(targetDistance < distanceLimit)
        {
            blendOut += distanceLimit - targetDistance;
        }
        Vector3 direction = Vector3.Slerp(targetDirection , aimDirection,blendOut);
        return aimTransform.position + direction;
    }

    private void AimAtTranform(Transform bone, Vector3 targetPosition,float weight)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection , targetDirection);
        Quaternion boneRotation = Quaternion.Slerp(Quaternion.identity,aimTowards, weight);
        bone.rotation = boneRotation * bone.rotation;

    }
    public void SetTargetTranform(Transform target)
    {
        targetTransform = target;
        Debug.Log("PlayerSet");
    }
    public void SetAimTranform(Transform aim)
    {
        aimTransform = aim;
        Debug.Log("WeaponSet");
    }
}
