using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waves
{
    public int enemyCount;
    public int enemySpawnInterval;
    public Transform enemy;
}

public class EnemyWaveSpawner : MonoBehaviour
{
    public enum SpawnState { waitingToStart, inProgress, waitingToEnd };

    public Waves[] waves = new Waves[4];
    public Transform[] spawnpoints = new Transform[4];

    Waves _waves;
    float timeBetweenWaves = 5f;
    public float waitTostartInterval;
    float nextspawntime;
    public float WaveEnemyCount;

    public int ongoingWavenumber = 0;

    SpawnState state = SpawnState.waitingToStart;

    private void Start()
    {
        waitTostartInterval = timeBetweenWaves;
        _waves = waves[ongoingWavenumber];
    }

    private void Update()
    {
            if (waitTostartInterval <= 0)
            {
                if (state == SpawnState.waitingToStart)
                {
                    state = SpawnState.inProgress;
                    WaveEnemyCount = _waves.enemyCount;
                    Debug.Log(Time.time);
                }
            }
            else
            {
                waitTostartInterval -= Time.deltaTime;
            }
            if (state == SpawnState.inProgress && nextspawntime < Time.time)
            {
                SpawnEnemy();
            }
            else if (state == SpawnState.waitingToEnd)
            {
                if (checkEnemyAlive() == false)
                {
                //waves.Remove(waves[0]);
                Debug.Log(Time.time);
                }
            }
    }

    private void SpawnEnemy()
    {
        Transform randomspawnPoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
        Instantiate(_waves.enemy, randomspawnPoint.transform.position, Quaternion.identity);
        WaveEnemyCount--;
        nextspawntime = Time.time + _waves.enemySpawnInterval;
        if (WaveEnemyCount == 0)
        {
            state = SpawnState.waitingToEnd;
        }
    }

    private bool checkEnemyAlive()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            return false;
        }
        return true;
    }
}
