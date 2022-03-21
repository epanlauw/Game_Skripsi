using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class DayAndNightCycle : MonoBehaviour
{
    public static DayAndNightCycle instance;

    [SerializeField] Gradient lightColor;

    public GameObject light2D;
    public int days = 0;
    public int tempDay = -1;
    public float time = 50;
    public Text timeText;
    public Text dayText;

    bool canChangeDay = true;
    int timeToRealTime;
    PlayerHealthManager player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        tempDay = -1;
        DontDestroyOnLoad(gameObject);
    }
 
    void Update()
    {
        if (time > 500)
        {
            time = 0;
        }

        if ((int)time == 250 && canChangeDay)
        {
            canChangeDay = false;
            days++;
        }

        if ((int)time == 255)
            canChangeDay = true;

        if (tempDay != days && SceneManager.GetActiveScene().name.Contains("Battle"))
        {
            WeatherSystem.instance.BuffEnemy();
            tempDay = days;
        }

        if (days > MarkovChain.instance.days)
        {
            player = FindObjectOfType<PlayerHealthManager>();
            player.LosePlayer();
        }

        time += Time.deltaTime;
        timeToRealTime = Mathf.FloorToInt((time * 0.048f) + 12);
        if (timeToRealTime == 24)
        {
            timeToRealTime = 0;
        }
        else if (timeToRealTime > 24)
        {
            timeToRealTime -= 24;
        }

        if(timeToRealTime <= 9)
        {
            timeText.text = "0" + (timeToRealTime).ToString() + ":00";
        }
        else
        {
            timeText.text = (timeToRealTime).ToString() + ":00";
        }
        dayText.text = "Days:" + (days + 1).ToString() + "/" + MarkovChain.instance.days;

        light2D.GetComponent<Light2D>().color = lightColor.Evaluate(time * 0.002f);
    }
}
