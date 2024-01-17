using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BulletsPoolManager : MonoBehaviour
{
    [SerializeField] private int amountToPool = 30;
    [SerializeField] private GameObject BulletsPrefab;
    public static BulletsPoolManager instance;
    private List<GameObject> PoolgameObjects = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        { instance = this; }
    }
    void Start()
    {
        for(int i = 0; i < amountToPool; i++) 
        {
            GameObject obj = Instantiate(BulletsPrefab);
            obj.SetActive(false);
            PoolgameObjects.Add(obj);
        }

    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < PoolgameObjects.Count;i++)
        {
            if (!PoolgameObjects[i].activeInHierarchy)
            {
                return PoolgameObjects[i];
            }
        }
        return null;
    }
}
