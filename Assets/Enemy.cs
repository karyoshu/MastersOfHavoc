using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float StartSpeed = 3;
    public float health = 100;
    public GameObject DeathEffect;
    public GameObject Bones;
    public float Damage = 3;
    public float attackRate = 1;
    Animator anim;
    AIPath aiPath;
    private float attackCountDown;
    private bool attacking;

    private void Awake()
    {
        aiPath = GetComponent<AIPath>();
        aiPath.speed = StartSpeed;
        aiPath.target = EnemyManager.Instance.Target;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            GameObject effect = Instantiate(DeathEffect, transform.position, transform.rotation);
            GameObject bones = Instantiate(Bones, transform.position, transform.rotation);
            Destroy(bones, 2);
            Destroy(effect, 1f);
            Die();
        }
    }

    void Die()
    {
        EnemyManager.Instance.currentEnemies.Remove(gameObject);
        Destroy(gameObject);
    }

    public void Slow(float amount)
    {
        aiPath.speed = StartSpeed * (1f - amount);
        Invoke("ResetSpeed", 0.3f);
    }

    private void ResetSpeed()
    {
        CancelInvoke();
        aiPath.speed = StartSpeed;
    }

    private void Update()
    {
        if (aiPath.TargetReached)
            attacking = true;
        anim.SetBool("Walking", false);
        if (attacking)
        {
            if (attackCountDown <= 0)
            {
                Attack();
                attackCountDown = 1f / attackRate;
            }
            attackCountDown -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        anim.Play("MeleeAttack");
        PlayerStats.Instance.Health -= Damage;
    }
}
