/*
 * Enemy weapon controls
 * 
 * by Wendy Slattery, Luca Labruna
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator enemyWeapon;

    public float rateOfFire = 3f;
    public float fireDelay = 3f;
    float fireRate;
    float nextFire;

    private void Start()
    {
        fireRate = rateOfFire;
        nextFire = Time.time;
        DelayStartFire();
    }    

    void Update()
    {
        CheckIfTimeToFire();        
    }

    private void DelayStartFire()
    {
        nextFire = nextFire + fireDelay;
    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            Shoot();
            nextFire = Time.time + fireRate;
            enemyWeapon.SetTrigger("WaterShoot");
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
