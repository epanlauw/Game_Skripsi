using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AreaExit : MonoBehaviour
{
    public string areaToLoad;
    public string areaTransitionName;

    public AreaEntrance theEntrance;

    public float waitToLoad = 1f;
    public int index;

    bool shouldLoadAfterFade;
    AudioPlay audioPlay;
    // Start is called before the first frame update
    void Start()
    {
        theEntrance.transitionName = areaTransitionName;

        audioPlay = FindObjectOfType<AudioPlay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            shouldLoadAfterFade = true;
            UIFade.instance.FadeToBlack();

            PlayerController.instance.areaTransitionName = areaTransitionName;

            audioPlay.PlaySong(index);
        }
    }
}
