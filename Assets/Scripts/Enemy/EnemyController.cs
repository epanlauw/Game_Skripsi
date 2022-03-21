using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator anim;
    private Transform target;

    public float speed;
    public float maxRange;
    public float minRange;
    public Transform homePos;

    public Transform[] patrolPoints;
    private Transform currentPatrolPoint;
    private int currentPatrolIndex;
    private bool isGoHome = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        {
            FollowPlayer();
        }
        else if (Vector3.Distance(target.position, transform.position) >= maxRange)
        {
            if (isGoHome)
            {
                Patrol();
            }
            else
            {
                GoHome();
            }
        }
    }

    public void FollowPlayer()
    {
        isGoHome = false;
        anim.SetBool("isMove", true);
        anim.SetFloat("moveX", (target.position.x - transform.position.x));
        anim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime);
    }

    public void GoHome()
    {
        isGoHome = true;
        anim.SetBool("isMove", true);
        anim.SetFloat("moveX", (homePos.position.x - transform.position.x));
        anim.SetFloat("moveY", (homePos.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            anim.SetBool("isMove", false);
        }
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
