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
    public enum SpawnState { waitingToStart, inProgress, waitingToEnd, calculatingTime };

    public Waves[] waves = new Waves[4];
    public Transform[] FlyingEnemyspawnpoints = new Transform[4];
    public Transform[] Canonspawnpoints = new Transform[4];
    public Transform[] Blobspawnpoints = new Transform[3];

    Waves _waves;
    float timeBetweenWaves = 5f;
    public float waitTostartInterval;
    float nextspawntime;
    public float WaveEnemyCount;

    public int ongoingWavenumber = 0;

    public float wavestarttime;
    public float waveendtime;

    int index = 0;

    Transform randomspawnPoint;

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
                wavestarttime = Time.time;
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
                waveendtime = Time.time;
                state = SpawnState.calculatingTime;
                CalculateLavaDecrease();
            }
        }
    }

    private void CalculateLavaDecrease()
    {
        float elapsedwavetime = waveendtime - wavestarttime;
        Debug.Log(elapsedwavetime);
        if (elapsedwavetime < 30)
        {
            //total size/3
        }
        else if (elapsedwavetime > 30 || elapsedwavetime < 60)
        {
            //total size/1.5
        }
        state = SpawnState.waitingToStart;
        waitTostartInterval = 5f;
        ongoingWavenumber += 1;
        _waves = waves[ongoingWavenumber];
    }

    private void SpawnEnemy()
    {
        Debug.Log(index);
        if(ongoingWavenumber == 0)
        {
            randomspawnPoint = FlyingEnemyspawnpoints[Random.Range(0, FlyingEnemyspawnpoints.Length)];
            Instantiate(_waves.enemy, randomspawnPoint.transform.position, Quaternion.identity);
        }
        else if(ongoingWavenumber == 1)
        {
            Instantiate(_waves.enemy, Blobspawnpoints[index].transform.position, Quaternion.identity);
            index += 1;
        }
        else if(ongoingWavenumber == 2)
        {
            Instantiate(_waves.enemy, Canonspawnpoints[index].transform.position, Quaternion.identity);
            index += 1;
        }
        WaveEnemyCount--;
        nextspawntime = Time.time + _waves.enemySpawnInterval;
        if (WaveEnemyCount == 0)
        {
            state = SpawnState.waitingToEnd;
            index = 0;
        }

    }

    private bool checkEnemyAlive()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Blob").Length == 0)
        {
            return false;
        }
        return true;
    }
}
