using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRb;

    public int damageToGive;
    public GameObject damageNumber;
    public float bulletLive = 2f;
    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 dir = (target.transform.position - transform.position).normalized * speed;
        bulletRb.velocity = new Vector2(dir.x, dir.y);
        Destroy(gameObject, bulletLive);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
                Transform pos = other.gameObject.GetComponent<Transform>();
                var clone = (GameObject)Instantiate(damageNumber, pos.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingNumbers>().damageNumber = damageToGive;
            }
            Destroy(gameObject);
        }
    }
}
