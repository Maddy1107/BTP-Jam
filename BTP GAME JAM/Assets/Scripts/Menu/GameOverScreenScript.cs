using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenScript : MonoBehaviour
{
    public void restart()
    {
        SceneManager.LoadScene(1);
    }

    public void returnToMain()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<AudioManager>().Stop("GameSound");
        FindObjectOfType<AudioManager>().Play("Menu");
    }
}
