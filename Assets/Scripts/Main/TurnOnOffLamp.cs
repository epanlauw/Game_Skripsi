using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TurnOnOffLamp : MonoBehaviour
{
    public GameObject[] point2D;

    private float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time = DayAndNightCycle.instance.time;

        if(time > 150 && time <= 380)
        {
            for(int i = 0; i < point2D.Length; i++)
            {
                point2D[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < point2D.Length; i++)
            {
                point2D[i].SetActive(false);
            }
        }
    }
}
