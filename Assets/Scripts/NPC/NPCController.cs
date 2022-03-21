using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float speed;
    public float left, right, up, down;
    public bool isHorizontal, isVertical;

    Animator anim;

    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogManager.instance.npcMove)
        {
            if (isHorizontal)
            {
                MovementHorizontal();
            }
            else if (isVertical)
            {
                MovementVertical();
            }
        }
        else
        {
            anim.SetBool("isMove", false);
        }
    }

    public void MovementHorizontal()
    {
        if (transform.position.x <= left)
        {
            dir = Vector3.right;
            anim.SetFloat("moveX", dir.x);
            anim.SetBool("isMove", true);
        }
        else if (transform.position.x >= right)
        {
            dir = Vector3.left;
            anim.SetFloat("moveX", dir.x);
            anim.SetBool("isMove", true);
        }
        transform.position += dir * speed * Time.deltaTime;
    }

    public void MovementVertical()
    {
        if (transform.position.y <= down)
        {
            dir = Vector3.up;
            anim.SetFloat("moveY", dir.y);
            anim.SetBool("isMove", true);
        }
        else if (transform.position.y >= up)
        {
            dir = Vector3.down;
            anim.SetFloat("moveY", dir.y);
            anim.SetBool("isMove", true);
        }
        transform.position += dir * speed * Time.deltaTime;
    }
}
