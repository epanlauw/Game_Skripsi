using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public GameObject loseScreen;
    public GameObject pauseMenu;
    public void Restart()
    {
        SceneManager.LoadScene("Gameplay1");
        loseScreen.SetActive(false);
        /*if(PlayerController.instance != null)
        {
            Destroy(PlayerController.instance.gameObject);
        }*/
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        loseScreen.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        if (UIFade.instance != null)
        {
            Destroy(UIFade.instance.gameObject);
        }

        if (PlayerController.instance != null)
        {
            GameObject camera = GameObject.Find("Custom Camera");
            camera.GetComponent<CameraController>().enabled = false;
            Destroy(PlayerController.instance.gameObject);
        }

        if (DayAndNightCycle.instance != null)
        {
            Destroy(DayAndNightCycle.instance.gameObject);
        }

        if (WeatherSystem.instance != null)
        {
            Destroy(WeatherSystem.instance.gameObject);
        }

        if (MarkovChain.instance != null)
        {
            Destroy(MarkovChain.instance.gameObject);
        }
    }
}
