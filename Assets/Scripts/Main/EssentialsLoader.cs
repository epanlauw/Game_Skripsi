using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject player;
    public GameObject dayNight;
    public GameObject weatherSystem;
    public GameObject markovChain;

    // Start is called before the first frame update
    void Start()
    {
        if (UIFade.instance == null)
        {
            Instantiate(UIScreen);
        }

        if (PlayerController.instance == null)
        {
            Instantiate(player);
        }

        if(DayAndNightCycle.instance == null)
        {
            Instantiate(dayNight);
        }

        if(WeatherSystem.instance == null)
        {
            Instantiate(weatherSystem);
        }

        if(MarkovChain.instance == null)
        {
            Instantiate(markovChain);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
