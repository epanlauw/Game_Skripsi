using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string transitionName;

    // Start is called before the first frame update
    void Start()
    {
        if(transitionName == PlayerController.instance.areaTransitionName)
        {
            PlayerController.instance.transform.position = new Vector3(transform.position.x, transform.position.y, PlayerController.instance.transform.position.z);
            WeatherSystem.instance.BuffEnemy();
            if (transitionName == "Cave-1")
            {
                WeatherSystem.instance.gameObject.SetActive(!WeatherSystem.instance.gameObject.activeInHierarchy);
                // transform.parent.gameObject.SetActive(false);
            }
        }

        UIFade.instance.FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
