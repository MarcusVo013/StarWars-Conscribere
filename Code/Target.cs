using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Camera maincamera;
    Ray ray;
    RaycastHit hitinfo;
    // Start is called before the first frame update
    void Start()
    {
        maincamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = maincamera.transform.position;
        ray.direction = maincamera.transform.forward;
        Physics.Raycast(ray, out hitinfo);
        //transform.position = hitinfo.point;
        if(Physics.Raycast(ray, out hitinfo)) { transform.position = hitinfo.point; }
        else { transform.position = ray.origin + ray.direction * 1000.0f; }
    }
}
