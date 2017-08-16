using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{

    public int[] EnemyWaves;
    public float timeBetweenEachWaves = 30;
    public GameObject EnemyPrefab;
    public Transform Target;
    public float SpawnDistance = 10;
    public int currentWave = 0;
    public float CountDown;
    public bool spawning = false;

    public List<GameObject> currentEnemies = new List<GameObject>();

    public void Start()
    {
        CountDown = timeBetweenEachWaves;
    }

    public IEnumerator StartSpawningWave()
    {
        int spawnedCount = 0;
        spawning = true;
        while (spawnedCount < EnemyWaves[currentWave])
        {
            spawnedCount++;
            GameObject enemy = Instantiate(EnemyPrefab, GetPointOncircle(Target.position, Random.Range(0, 360f), SpawnDistance), Quaternion.identity, transform);
            currentEnemies.Add(enemy);
            yield return new WaitForSeconds(1f);
        }
        //CountDown = timeBetweenEachWaves;
        //spawning = false;
        currentWave++;
    }

    public Vector3 GetPointOncircle(Vector3 center, float angleDegrees, float radius)
    {
        // initialize calculation variables
        float _x = 0;
        float _z = 0;
        float angleRadians = 0;
        Vector3 _returnVector;
        // convert degrees to radians
        angleRadians = angleDegrees * Mathf.PI / 180.0f;
        // get the 2D dimensional coordinates
        _x = radius * Mathf.Cos(angleRadians);
        _z = radius * Mathf.Sin(angleRadians);
        // derive the 2D vector
        _returnVector = new Vector3(_x, 1, _z) + center;
        // return the vector info
        return _returnVector;
    }

    private void Update()
    {
        if (currentEnemies.Count == 0 && CountDown <= 0)
        {
            spawning = false;
            CountDown = timeBetweenEachWaves + currentWave * 5;
            UIManager.Instance.ShowWaveClearedMessage();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(StartSpawningWave());
        }

        if (!spawning)
        {
            CountDown -= Time.deltaTime;
            CountDown = Mathf.Clamp(CountDown, 0, Mathf.Infinity);
        }
        if(CountDown <= 0 && !spawning)
            StartCoroutine(StartSpawningWave());
    }

}
