using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float fireRate;
    public string enemyTag = "Enemy";
    public float range;
    private Transform target;
    private Enemy targetEnemy;

    public AIPath aiPath;
    public bool standing;
    private float fireCountDown;
    public float rotationSpeed;
    public GameObject BulletPrefab;
    public Transform FirePoint;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        InvokeRepeating("UpdateTarget", 0, 0.2f);
    }

    private void Update()
    {
        //standing = aiPath.TargetReached;

        if (target == null)
        {
            return;
        }
        LockOnTarget();

        if (fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(target);
    }

    private void UpdateTarget()
    {
        if (!standing)
        {
            target = null;
            return;
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
            targetEnemy = null;
        }
    }
}
