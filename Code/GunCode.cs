using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GunCode : MonoBehaviour
{
    //bullet
    [SerializeField] private GameObject Bullet;

    //force
    [SerializeField] float ShootForce, UpwardForce;

    //GunState
    [SerializeField] float TimeBetweenShooting, spread, reloadTime, TimeBetweenShoot;
    [SerializeField] int magazineSize, bulletPerTap;
    [SerializeField] bool AllowButtonHold;
    int bulletsLeft, BulletsShot;

    //Bools
    bool shooting, readyToShoot, Realoading;

    //Referece
    [SerializeField] Camera Cam;
    [SerializeField] Transform AtkPoint;
    [SerializeField] bool AllowInvoke = true;
    //Graphics
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] TextMeshPro ammunitionDisplay;
    private void Awake()
    {
        //magezine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();

        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletPerTap + " / " + magazineSize + bulletPerTap);
    }
    private void MyInput()
    {
        //Allow To hold button and take corresponding input
        if (AllowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //shooting
        if(readyToShoot && shooting && !Realoading && bulletsLeft > 0)
        {
            //Set bullets shoot to 0 
            BulletsShot = 0;
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = true;
        Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //If Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !Realoading) Reload();
        if (readyToShoot && shooting && !Realoading && bulletsLeft <= 0) Reload();

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);
        Vector3 directionWithoutSpread = targetPoint - AtkPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(Bullet, AtkPoint.position, Quaternion.identity);

        currentBullet.transform.forward = directionWithSpread.normalized;

        if (muzzleFlash != null)
            Instantiate(muzzleFlash, AtkPoint.position, Quaternion.identity);

        //currentBullet.GetComponents<Rigidbody>().AddForce(directionWithSpread.normalized * ShootForce,ForceMode.Impulse);

        bulletsLeft--;
        BulletsShot++;
        if(AllowInvoke)
        {
            Invoke("ResetShot", TimeBetweenShooting);
            AllowInvoke = false;
        }
        if (BulletsShot < bulletPerTap && bulletsLeft > 0)
            Invoke("Shoot", TimeBetweenShoot);
    }
   private void ResetShoot()
    {
        readyToShoot = true;
        AllowInvoke = true;
    }
    private void Reload()
    {
        Realoading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void RealoadIsFinished()
    {
        bulletsLeft = magazineSize;
        Realoading = false;
    }
}
