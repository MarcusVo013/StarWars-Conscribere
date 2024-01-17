using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class PlayerData 
{
    public int Scene;
    public float health;
    public float[] position;

    public PlayerHealth healthHealth;

    public PlayerData(PlayerHealth player)
    {
        health = player.currenthealth;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
