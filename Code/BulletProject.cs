using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProject : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private Transform GreenHit;
    [SerializeField] private Transform RedHit;
    private Rigidbody Rigidbody;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Rigidbody.velocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TestTarget>() != null)
        { 
            Instantiate(GreenHit,transform.position, Quaternion.identity);       
        }
        else
        {
            Instantiate(RedHit,transform.position, Quaternion.identity);
        }
       gameObject.SetActive(false);
    }
}
