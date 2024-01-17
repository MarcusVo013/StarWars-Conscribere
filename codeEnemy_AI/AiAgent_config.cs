using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgent_config : ScriptableObject
{
    
    public string playerTag = "Player";
    public string move_Parameter_Animator = "Speed";
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    public float dieForce = 5.0f;
    public float maxSightDistance = 5.0f;
    //public float ai_Health = 100f;
    public float stoppingDistance = 1.0f;
    public float atkSpeed = 1.0f;
    public float pickUpSpeed = 7.0f;
    public float chaseSpeed = 5;
    public float fireDistance = 7;
}
