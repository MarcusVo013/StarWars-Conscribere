using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InfinitySpawn : MonoBehaviour
{
    float xPos = 0;
    float zPos = 0;
    public GameObject objectToSpawn;
    public Transform maxSpawnRange;
    public Transform minSpawnRange;
    [SerializeField] float yPos = 0;
    [SerializeField] float spawnTime = 1.0f;
    [SerializeField] int objectCount;
    [SerializeField] int objectSpawnNumber = 10;
    public AudioClip startSound;
    public AudioSource audioSource;
    void Start()
    {
        StartCoroutine(InfinitySpawner());
        DestroyController.onDestroy += () => objectCount--;
        audioSource.PlayOneShot(startSound);
    }

    IEnumerator InfinitySpawner()
    {
        while (true)
        {
            if (objectCount < objectSpawnNumber)
            {
                Vector3 min = minSpawnRange.position;
                Vector3 max = maxSpawnRange.position;

                xPos = Random.Range(min.x, max.x);
                zPos = Random.Range(min.z, max.z);
                GameObject spawnObject = Instantiate(objectToSpawn, new Vector3(xPos, yPos, zPos), Quaternion.identity);                
                objectCount += 1;

            }

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
