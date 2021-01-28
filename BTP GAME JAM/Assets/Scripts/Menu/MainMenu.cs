using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject[] slideshows;
    public int index = 0;

    private void Update()
    {
        slideshows[index].SetActive(true);
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DetailsMenuplus()
    {
        slideshows[index].SetActive(false);
        index += 1;
        if (index > 2)
        {
            index = 0;
        }
    }

    public void DetailsMenuminus()
    {
        slideshows[index].SetActive(false);
        index -= 1;
        if (index < 0)
        {
            index = 2;
        }
    }

    public void OpenYoutube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCVHyKDqqEf310KkWBHeKhBQ");
    }

    public void OpenFacebook()
    {
        Application.OpenURL("https://www.facebook.com/nilankar.deb2/");
    }

    public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com/nil.maddy11/");
    }
}
