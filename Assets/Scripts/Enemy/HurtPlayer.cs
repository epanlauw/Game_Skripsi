using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive;
    public GameObject damageNumber;

    private float waitToHurt = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponent<PlayerHealthManager>().playerCurrentHealth > 0)
            {
                other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
                Transform pos = other.gameObject.GetComponent<Transform>();
                var clone = (GameObject)Instantiate(damageNumber, pos.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingNumbers>().damageNumber = damageToGive;
            }
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerHealthManager>().playerCurrentHealth > 0)
            {
                waitToHurt -= Time.deltaTime;
                if (waitToHurt <= 0)
                {
                    other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
                    Transform pos = other.gameObject.GetComponent<Transform>();
                    var clone = (GameObject)Instantiate(damageNumber, pos.position, Quaternion.Euler(Vector3.zero));
                    clone.GetComponent<FloatingNumbers>().damageNumber = damageToGive;
                    waitToHurt = 1f;
                }
            }
        }
    }
}
