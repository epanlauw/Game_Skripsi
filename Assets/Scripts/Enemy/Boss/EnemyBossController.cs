using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : MonoBehaviour
{
    private Animator anim;
    private Transform target;
    private bool isGoHome;
    private EnemyHealthManager bossHealth;

    public float speed;
    public float maxRange;
    public float minRange;
    public Transform homePos;

    // Shooter
    public float fireRate = 1f;
    private float nextFireTime;

    public GameObject bulletMain;
    public Transform shooterTransform;

    public GameObject bulletTwo;
    public Transform[] shooterPhaseTwo;

    // Patrol
    public Transform[] patrolPoints;
    private Transform currentPatrolPoint;
    private int currentPatrolIndex;

    private AudioPlay audioPlay;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        bossHealth = GetComponent<EnemyHealthManager>();
        audioPlay = FindObjectOfType<AudioPlay>();

        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bossHealth.enemyCurrentHealth > (bossHealth.enemyMaxHealth  * 0.75f))
        {
            FollowPlayer();
        }
        else if(bossHealth.enemyCurrentHealth > (bossHealth.enemyMaxHealth * 0.65f))
        {
            if (isGoHome && nextFireTime < Time.time)
            {
                FiringPhaseOne();
            }
            else
            { 
                GoHome();
            }
        }
        else if(bossHealth.enemyCurrentHealth > (bossHealth.enemyMaxHealth * 0.5f))
        {
            if(nextFireTime < Time.time)
            {
                bulletMain = Resources.Load<GameObject>("Prefabs/Projectile Enemy/Rock");
                FiringPhaseOne();
            }
        }
        else if(bossHealth.enemyCurrentHealth > (bossHealth.enemyMaxHealth * 0.25f))
        {
            if (nextFireTime < Time.time)
                FiringPhaseTwo();
        }
        else if (bossHealth.enemyCurrentHealth > 0f)
        {
            Patrol();
            fireRate = 0.9f;
            bulletTwo.GetComponent<BulletEnemy>().speed = 6f;
            bulletTwo.GetComponent<BulletEnemy>().damageToGive = 10;
            if(nextFireTime < Time.time)
            {
                FiringPhaseTwo();
            }
        }
    }

    void FollowPlayer()
    {
        isGoHome = false;
        anim.SetBool("isMove", true);
        anim.SetFloat("moveX", (target.position.x - transform.position.x));
        anim.SetFloat("moveY", (target.position.y - transform.position.y));
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
    }

    void GoHome()
    {
        anim.SetBool("isMove", true);
        anim.SetFloat("moveX", (homePos.position.x - transform.position.x));
        anim.SetFloat("moveY", (homePos.position.y - transform.position.y));
        
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            anim.SetBool("isMove", false);
            isGoHome = true;
        }
    }

    void FiringPhaseOne()
    {
        Instantiate(bulletMain, shooterTransform.position, shooterTransform.rotation);
        nextFireTime = Time.time + fireRate;
    }

    void FiringPhaseTwo()
    {
        for(int i = 0; i < shooterPhaseTwo.Length; i++)
        {
            Instantiate(bulletTwo, shooterPhaseTwo[i].position, shooterTransform.rotation);
        }
        nextFireTime = Time.time + fireRate;
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, currentPatrolPoint.position) < 0.1f)
        {
            if (currentPatrolIndex + 1 < patrolPoints.Length)
            {
                currentPatrolIndex++;
            }
            else
            {
                currentPatrolIndex = 0;
            }
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
        }

        anim.SetBool("isMove", true);
        anim.SetFloat("moveX", (currentPatrolPoint.position.x - transform.position.x));
        anim.SetFloat("moveY", (currentPatrolPoint.position.y - transform.position.y));

        transform.position = Vector3.MoveTowards(transform.position, currentPatrolPoint.position, speed * Time.fixedDeltaTime);
    }
}
