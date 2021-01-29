using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameplay;
    public bool gameOver;
    public bool gamewin;

    public EnemyWaveSpawner enemySpawner;

    public TextMeshProUGUI countdown;

    public TextMeshProUGUI LevelDisplay;

    public GameObject gameOverHUD;

    public GameObject gameWinHUD;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemyWaveSpawner>();
        gameOver = false;
        gameplay = false;
        gamewin = false;
        StartCoroutine(Countdown());
    }

    public void StartGame()
    {
        gameplay = true;
    }

    public void pauseGamePlay()
    {
        gameplay = false;
    }

    public void LostGAme()
    {
        gameplay = false;
        gameOver = true;
        gameOverHUD.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Play("GameOver");
    }

    public void Winagme()
    {
        gameplay = false;
        gamewin = true;
        gameWinHUD.gameObject.SetActive(true);
    }

    public void coroutinestart()
    {
        LevelDisplay.enabled = true;
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        LevelDisplay.SetText("LEVEL " + (enemySpawner.ongoingWavenumber + 1).ToString());

        yield return new WaitForSeconds(1);

        countdown.gameObject.SetActive(true);
        LevelDisplay.enabled = false;

        while (enemySpawner.waitTostartInterval >  1)
        {
            countdown.text = ((int)enemySpawner.waitTostartInterval).ToString();

            yield return new WaitForSeconds(1f);
        }
        StartGame();
        countdown.text = "Kill all\nMonsters!!!";

        yield return new WaitForSeconds(1f);

        countdown.gameObject.SetActive(false);
    }
}
