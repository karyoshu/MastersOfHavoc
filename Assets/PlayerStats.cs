using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    public int currentResources = 100;

    public int CurrentResources
    {
        get
        {
            return currentResources;
        }
        set
        {
            currentResources = value;
            UIManager.Instance.UpdateResourceCount();
        }
    }

    public float health;
    public float MAX_HEALTH = 10000;

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            UIManager.Instance.UpdateHealth();
        }
    }

    private void Start()
    {
        health = MAX_HEALTH;
        InvokeRepeating("IncrementResource", 5, 1);
    }

    void IncrementResource()
    {
        CurrentResources += (EnemyManager.Instance.currentWave + 1);
    }
}
