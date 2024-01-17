using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class CharacterConfig : ScriptableObject
{
    [Header("___Health Option___")]
    public float Health =100;

    [Header("___Motion Option___")]
    public float GroundSpeed =1;
}
