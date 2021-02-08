using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public Transform[] Blobspawnpoints = new Transform[2];
    public Transform[] Bossspawnpoints = new Transform[4];

    Waves _waves;
    float timeBetweenWaves = 5f;
    public float waitTostartInterval;
    float nextspawntime;
    public float WaveEnemyCount;

    public int ongoingWavenumber = 0;

    public float wavestarttime;
    public float waveendtime;

    int index = 0;

    public GameObject levelComplete;
    public TextMeshProUGUI TimeTaken;

    Transform randomspawnPoint;

    public Text flyHowto;
    public Text blobHowTo;
    public Text canonHowTo;
    public Text bossHowTo;

    public GameObject Lava;

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
                levelComplete.gameObject.SetActive(true);
                GameManager.instance.pauseGamePlay();
                flyHowto.gameObject.SetActive(false);
                blobHowTo.gameObject.SetActive(false);
                canonHowTo.gameObject.SetActive(false);
                bossHowTo.gameObject.SetActive(false);
            }
        }
    }

    private void CalculateLavaDecrease()
    {
        float distance1 = 0.5f;
        float distance2 = 1f;
        float elapsedwavetime = waveendtime - wavestarttime;
        Debug.Log(elapsedwavetime);

        if(elapsedwavetime < 60 && elapsedwavetime >= 0)
        {
            TimeTaken.SetText("Time Taken :- " + elapsedwavetime.ToString() + "s");
        }
        else
        {
            TimeTaken.SetText("Time Taken :- " + (elapsedwavetime/60).ToString() + "m" + (elapsedwavetime % 60).ToString() + "s");
        }
        if (elapsedwavetime < 30)
        {
            while (distance2 > 0)
            {
                Vector2 Lavay = Lava.transform.position;
                Lavay.y -= 0.1f;
                Lava.transform.position = Lavay;
                distance2 -= 0.1f;
            }
            distance2 = 1.5f;
        }
        else if (elapsedwavetime > 30 || elapsedwavetime < 60)
        {
            while (distance1 > 0)
            {
                Vector2 Lavay = Lava.transform.position;
                Lavay.y -= 0.1f;
                Lava.transform.position = Lavay;
                distance1 -= 0.1f;
            }
            distance1 = 0.5f;
        }
    }

    public void StartNewLevel()
    {
        state = SpawnState.waitingToStart;
        waitTostartInterval = 5f;
        ongoingWavenumber += 1;
        _waves = waves[ongoingWavenumber];
        GameManager.instance.coroutinestart();
    }

    private void SpawnEnemy()
    {
        if(ongoingWavenumber == 0)
        {
            randomspawnPoint = FlyingEnemyspawnpoints[Random.Range(0, FlyingEnemyspawnpoints.Length)];
            Instantiate(_waves.enemy, randomspawnPoint.transform.position, Quaternion.identity);
        }
        else if(ongoingWavenumber == 1)
        {
            Instantiate(_waves.enemy, Blobspawnpoints[index].transform.position, Quaternion.identity);
            //index += 1;
        }
        else if(ongoingWavenumber == 2)
        {
            Instantiate(_waves.enemy, Canonspawnpoints[index].transform.position, Quaternion.identity);
            index += 1;
        }
        else if (ongoingWavenumber == 3)
        {
            Instantiate(_waves.enemy, Bossspawnpoints[index].transform.position, Quaternion.identity);
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
        if (GameObject.FindGameObjectsWithTag("FlyingEnemy").Length == 0 && GameObject.FindGameObjectsWithTag("Blob").Length == 0 
            && GameObject.FindGameObjectsWithTag("Canon").Length == 0 && GameObject.FindGameObjectsWithTag("Boss").Length == 0)
        {
            return false;
        }
        return true;
    }

    public void HowToOpen()
    {
        if(ongoingWavenumber == 0)
        {
            if (flyHowto.gameObject.activeSelf == false)
            {
                flyHowto.gameObject.SetActive(true);
            }
            else
            {
                flyHowto.gameObject.SetActive(false);
            }
        }
        else if(ongoingWavenumber == 1)
        {
            if (blobHowTo.gameObject.activeSelf == false)
            {
                blobHowTo.gameObject.SetActive(true);
            }
            else
            {
                blobHowTo.gameObject.SetActive(false);
            }
        }
        else if(ongoingWavenumber == 2)
        {
            if (canonHowTo.gameObject.activeSelf == false)
            {
                canonHowTo.gameObject.SetActive(true);
            }
            else
            {
                canonHowTo.gameObject.SetActive(false);
            }
        }    
        else
        {
            if (bossHowTo.gameObject.activeSelf == false)
            {
                bossHowTo.gameObject.SetActive(true);
            }
            else
            {
                bossHowTo.gameObject.SetActive(false);
            }
        }
    }
}
