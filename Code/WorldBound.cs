using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBound : MonoBehaviour
{
    [Header("___Partroling Option___")]
    [Header("_Max Range On Map_")]
    public Transform Max;
    [Header("_Min Range On Map_")]
    public Transform Min;
    public Vector3 RandomPosition()
    {
        Vector3 min = Min.position;
        Vector3 max = Max.position;

        Vector3 randomPosition = new Vector3(
        Random.Range(min.x, max.x),
        Random.Range(min.y, max.y),
        Random.Range(min.z, max.z));
        return randomPosition;
    }
}
