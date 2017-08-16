using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("General")]
    public float range = 15f;
    public int Rank = 1;
    public TurretType type;

    [Header("Use bullets (default)")]
    public float fireRate = 1f;
    public GameObject BulletPrefab;
    public float Damage = 30;
    private float fireCountDown = 0f;

    [Header("Unity Laser")]
    public bool UseLaser = false;
    public int DamageOverTime = 30;
    public float slowAmount = 0.5f;
    public LineRenderer lineRenderer;
    public ParticleSystem ImpactEffect;
    public Light ImpactLight;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform PartToRotate;
    public float rotationSpeed = 10;
    public Transform FirePoint;
    public ParticleSystem MuzzleFlash;
    
    private Transform target;
    private Enemy targetEnemy;
    // Use this for initialization

    void Start () {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (target == null)
        {
            if(UseLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }
                ImpactEffect.Stop();
                ImpactLight.enabled = false;
            }
            return;
        }
        LockOnTarget();

        if (UseLaser)
            Laser();
        else
        {
            if (fireCountDown <= 0)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }
	}

    private void Laser()
    {
        targetEnemy.TakeDamage(DamageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            ImpactEffect.Play();
            ImpactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, FirePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = FirePoint.position - target.position;
        ImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
        ImpactEffect.transform.position = target.position + dir.normalized * .35f;

    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.damageValue = Damage;
        if (bullet != null)
            bullet.Seek(target);
        if (MuzzleFlash != null)
            MuzzleFlash.Play();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void UpdateTarget()
    {
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

    public void Upgrade()
    {
        Rank++;
        range++;
        if (UseLaser)
            DamageOverTime += 3;
        else
            Damage += 5;
        fireRate += 0.15f;
    }
}
