using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    float xPos = 0;
    float zPos = 0;
    public GameObject enemyToSpawn;
    public Transform maxSpawnRange;
    public Transform minSpawnRange;
    [SerializeField] float yPos = 0;
    [SerializeField] float spawnTime = 1.0f;
    [SerializeField] int enemyCount;
    [SerializeField] int enemySpawnNumber = 10;

    void Start()
    {
        StartCoroutine(Spawner());
    }
    IEnumerator Spawner()
    {
        Vector3 min = minSpawnRange.position;
        Vector3 max = maxSpawnRange.position;
        while (enemyCount < enemySpawnNumber )
        {
            xPos = Random.Range(min.x,max.x);
            zPos = Random.Range(min.z,max.z);
            Instantiate(enemyToSpawn,new Vector3 (xPos , yPos , zPos), Quaternion.identity) ;
            yield return new WaitForSeconds(spawnTime);
            enemyCount += 1;
        }
    }
    
}
