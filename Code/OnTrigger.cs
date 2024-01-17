using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjectsToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject go in gameObjectsToEnable)
            {
                go.SetActive(true);
            }

            Invoke("DestroySelf", 0.1f);
        }
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
