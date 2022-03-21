using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivatorEnemy : MonoBehaviour
{
    public string[] lines;
    public GameObject bossBattle;

    bool canActivate;

    public GameObject areaExit;

    private CameraShaker camShaker;
    private AudioPlay audioPlay;

    // Start is called before the first frame update
    void Start()
    {
        camShaker = GameObject.Find("Custom Camera").GetComponent<CameraShaker>();
        audioPlay = FindObjectOfType<AudioPlay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            DialogManager.instance.ShowDialog(lines);
        }

        if (DialogManager.instance.endOfDialogue)
        {
            gameObject.SetActive(false);
            areaExit.SetActive(false);
            camShaker.Shake(0.1f, 1.5f);
            audioPlay.PlaySFX(35);
            Invoke("BossShowUp", 2f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }


    void BossShowUp()
    {
        bossBattle.SetActive(true);
    }
}
