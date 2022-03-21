using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherSystem : MonoBehaviour
{
    public static WeatherSystem instance;

    public List<GameObject> weather;
    public GameObject annoucement;
    public Text weatherText;
    public Text announceText;

    string[] weatherDays;
    int days;
    GameObject[] enemy;
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

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        weatherDays = MarkovChain.instance.statesPerTotalDays;
    }

    // Update is called once per frame
    void Update()
    {
        days = DayAndNightCycle.instance.days;
        weatherText.text = "Weather :" + weatherDays[days];
        for (int i = 0; i < weather.Count; i++)
        {
            if (weatherDays[days] == weather[i].name)
            {
                weather[i].SetActive(true);
            }
            else
            {
                weather[i].SetActive(false);
            }
        }
    }

    public void BuffEnemy()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        int[] tempHealth = new int[enemy.Length];
        if (enemy.Length > 0)
        {
            for(int i = 0; i < enemy.Length; i++)
            {
                tempHealth[i] = enemy[i].GetComponent<EnemyHealthManager>().enemyMaxHealth;
                Debug.Log(weatherDays[days]);
                if (weatherDays[days] == "Rainy")
                {

                    annoucement.SetActive(true);
                    announceText.text = " Slime     Power     UP. \nDragon And Snake Weaker!!";
                    Invoke("CloseAnnounce", 2f);
                    if (enemy[i].name.Contains("Slime"))
                    {
                        enemy[i].GetComponent<EnemyHealthManager>().enemyMaxHealth = 20;
                        enemy[i].GetComponent<EnemyHealthManager>().SetMaxHealth();
                        enemy[i].GetComponent<SpriteRenderer>().color = Color.red;

                    }
                    else if(enemy[i].name.Contains("Dragon") || enemy[i].name.Contains("Snake"))
                    {
                        enemy[i].GetComponent<EnemyHealthManager>().enemyMaxHealth = 20;
                        enemy[i].GetComponent<EnemyHealthManager>().SetMaxHealth();
                        enemy[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                    }
                    else
                    {
                        enemy[i].GetComponent<EnemyHealthManager>().enemyMaxHealth = tempHealth[i];
                        enemy[i].GetComponent<EnemyHealthManager>().SetMaxHealth();
                        enemy[i].GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
                else if(weatherDays[days] == "Sunny")
                {
                    annoucement.SetActive(true);
                    announceText.text = " Bamboo     Power     UP!!";
                    Invoke("CloseAnnounce", 2f);
                    if (enemy[i].name.Contains("Bamboo"))
                    {
                        enemy[i].GetComponent<EnemyHealthManager>().enemyMaxHealth = 25;
                        enemy[i].GetComponent<EnemyHealthManager>().SetMaxHealth();
                        enemy[i].GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    else
                    {
                        enemy[i].GetComponent<EnemyHealthManager>().enemyMaxHealth = tempHealth[i];
                        enemy[i].GetComponent<EnemyHealthManager>().SetMaxHealth();
                        enemy[i].GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
                else if(weatherDays[days] == "Windy" || weatherDays[days] == "Cloudy")
                {
                    annoucement.SetActive(true);
                    announceText.text = " Beast Weaker!!";
                    Invoke("CloseAnnounce", 2f);
                    if (enemy[i].name.Contains("Beast"))
                    {
                        enemy[i].GetComponent<EnemyHealthManager>().enemyMaxHealth = 30;
                        enemy[i].GetComponent<EnemyHealthManager>().SetMaxHealth();
                        enemy[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                    }

                    else
                    {
                        enemy[i].GetComponent<EnemyHealthManager>().enemyMaxHealth = tempHealth[i];
                        enemy[i].GetComponent<EnemyHealthManager>().SetMaxHealth();
                        enemy[i].GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
                else
                {
                    enemy[i].GetComponent<EnemyHealthManager>().enemyMaxHealth = tempHealth[i];
                    enemy[i].GetComponent<EnemyHealthManager>().SetMaxHealth();
                    enemy[i].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }

    void CloseAnnounce()
    {
        annoucement.SetActive(false);
    }
}
