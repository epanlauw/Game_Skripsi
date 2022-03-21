using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlMenu;
    public GameObject optionMenu;

    private AudioPlay audioPlay;

    // Start is called before the first frame update
    void Start()
    {
        audioPlay = FindObjectOfType<AudioPlay>();
        audioPlay.PlaySong(15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene("Gameplay1");
        audioPlay.PlaySong(0);
    }

    public void ControlMenu()
    {
        controlMenu.SetActive(true);
    }

    public void OptionMenu()
    {
        optionMenu.SetActive(true);
    }
    
    public void Credit()
    {
        SceneManager.LoadScene("Credit");
        audioPlay.PlaySong(11);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
