using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimalController : MonoBehaviour
{
    public float speed;
    public float left, right;

    Animator anim;
    SpriteRenderer spriteRenderer;

    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogManager.instance.npcMove)
        {
            MovementHorizontal();
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
            anim.SetBool("isMove", true);
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x >= right)
        {
            dir = Vector3.left;
            anim.SetBool("isMove", true);
            spriteRenderer.flipX = true;
        }
        transform.position += dir * speed * Time.deltaTime;
    }
}
