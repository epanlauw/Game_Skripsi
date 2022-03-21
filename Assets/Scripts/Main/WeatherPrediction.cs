using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherPrediction : MonoBehaviour
{
    public Text weatherToday;
    public Text prediction;

    public int dayTime;
    
    // Start is called before the first frame update
    void Start()
    {
        prediction.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var entry in MarkovChain.instance.weather)
        {
            string weatherday = entry.Key.Substring(entry.Key.IndexOf('.') + 1);
            dayTime = int.Parse(weatherday);

            if (DayAndNightCycle.instance.days == dayTime)
            {
                prediction.text = "";
                weatherToday.text = entry.Key.Substring(0, entry.Key.IndexOf('.')).ToUpper();
                for(int i = 0; i < entry.Value.Count; i++)
                {
                    prediction.text += MarkovChain.instance.states[i] + "\t:" + System.Math.Round((float)(entry.Value[i] * 100), 2).ToString() + "%\n";
                }
            }
        }
    }
}
