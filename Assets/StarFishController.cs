using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StarFishController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    bool attack;
    bool anticipation;
    bool facingRight;

    float attackSpeed = 7;
    void Start()
    {
        animator = GetComponent<Animator>();   
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        AnimationArranger();
    }

    void FixedUpdate()
    {
        Attack();
    }


    void AnimationArranger()
    {
        animator.SetBool("Attack", attack);
        animator.SetBool("Anticipation",anticipation);

    }

    void DirectionArranger()
    {
        if (!facingRight)
        {
           facingRight = true;
           spriteRenderer.flipX = true;
        }
        else
        {
           facingRight = false;
           spriteRenderer.flipX = false;
        }

    }

    void AnticipationEvent()
    {
        anticipation = true;
    }
    
    void AttackEvent()
    {
        
        StartCoroutine(waitForAttack());
    }
    void Attack()
    {
        if (attack)
        {
            if (facingRight)
            {
                rb.velocity = new Vector2(attackSpeed *100* Time.fixedDeltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-attackSpeed * 100 *Time.fixedDeltaTime , rb.velocity.y);
            }


        }
    }

    IEnumerator waitForAttack()
    {
        yield return new WaitForSeconds(0.5f);
        attack = true;
        anticipation = false;
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        
        attack = false;
        anticipation = false;

        DirectionArranger();
    }


    


}
