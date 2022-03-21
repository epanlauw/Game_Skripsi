using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed = 5f;
    public string areaTransitionName;
    public bool canMove = true;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public GameObject inventory;
    public GameObject shop;
    public Image weaponImage;
    public bool canControl = true;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private Transform weaponTransform;
    private Animator anim;
    private Rigidbody2D rigid;
    private float timeFire = 1f;
    private float nextFireTime;
    private AudioPlay audioPlay;

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
        anim = gameObject.GetComponent<Animator>();
        rigid = gameObject.GetComponent<Rigidbody2D>();

        weaponTransform = transform.Find("Weapon");
        audioPlay = FindObjectOfType<AudioPlay>();

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canControl)
        {
            Movement();
            if (Input.GetKeyDown(KeyCode.I))
            {
                inventory.SetActive(!inventory.activeInHierarchy);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                shop.SetActive(true);
            }

            ChangeWeapon();
        }
    }

    void Movement()
    {
        if (canMove)
        {
            rigid.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }

        anim.SetFloat("moveX", rigid.velocity.x);
        anim.SetFloat("moveY", rigid.velocity.y);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                anim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                anim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));

                anim.SetFloat("attackX", Input.GetAxisRaw("Horizontal"));
                anim.SetFloat("attackY", Input.GetAxisRaw("Vertical"));



                if (Input.GetAxisRaw("Vertical") == -1)
                {
                    weaponTransform.localPosition = new Vector2(-0.12f, -0.62f);
                }

                else if (Input.GetAxisRaw("Vertical") == 1)
                {
                    weaponTransform.localPosition = new Vector2(-0.12f, 0.62f);
                }

                else if (Input.GetAxisRaw("Horizontal") == 1)
                {
                    weaponTransform.localPosition = new Vector2(0.52f, -0.25f);
                }

                else if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    weaponTransform.localPosition = new Vector3(-0.52f, -0.25f);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F) && nextFireTime < Time.time)
            {
                Firing();
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    void Firing()
    {
        nextFireTime = Time.time + timeFire;
        anim.SetTrigger("isAttack");
        GameObject bullet = Instantiate(bulletPrefab, weaponTransform.position, weaponTransform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (anim.GetFloat("lastMoveY") == -1)
        {
            bullet.transform.localRotation = Quaternion.Euler(0f, 0f, 270f);
            rb.AddForce(-weaponTransform.up * bulletForce, ForceMode2D.Impulse);
        }

        else if (anim.GetFloat("lastMoveY") == 1)
        {
            bullet.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            rb.AddForce(weaponTransform.up * bulletForce, ForceMode2D.Impulse);
        }

        else if (anim.GetFloat("lastMoveX") == 1)
        {
            bullet.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            rb.AddForce(weaponTransform.right * bulletForce, ForceMode2D.Impulse);
        }

        else if (anim.GetFloat("lastMoveX") == -1)
        {
            bullet.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
            rb.AddForce(-weaponTransform.right * bulletForce, ForceMode2D.Impulse);
        }

        audioPlay.PlaySFX(8);
    }

    void ChangeWeapon()
    {
        if (bulletPrefab.name.Contains("Kunai"))
        {
            weaponImage.sprite = bulletPrefab.GetComponent<SpriteRenderer>().sprite;
            weaponImage.rectTransform.sizeDelta = new Vector2(142.3727f, 60.1196f);
        }
        else
        {
            weaponImage.sprite = bulletPrefab.GetComponent<SpriteRenderer>().sprite;
            weaponImage.rectTransform.sizeDelta = new Vector2(100f, 100f);
        }
    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(.5f, .5f, 0f);
        topRightLimit = topRight + new Vector3(-.5f, -.5f, 0f);
    }
}
