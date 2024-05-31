using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StarFishController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Collider2D col ;

    bool attack;
    bool anticipation;
    bool facingRight;

    bool isAttackable;
    bool isAlive = true;
    bool damage;

    float attackSpeed = 7;
    void Start()
    {
        animator = GetComponent<Animator>();   
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        AnimationArranger();
      
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            
            Attack();
        }
       

    }


    void AnimationArranger()
    {
        animator.SetBool("Attack", attack);
        animator.SetBool("Anticipation",anticipation);
        animator.SetBool("isAlive", isAlive );

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

 

    void DeathEvent()
    {
        isAlive = false;
        col.enabled = false;
    }

    void RebornEvent()
    {
        col.enabled = true;
        attack = false;
        anticipation = false;
        isAlive = true;
    }


    IEnumerator waitForAttack()
    {
        yield return new WaitForSeconds(0.5f);
        attack = true;
        anticipation = false;
        isAttackable = false;
    }
    

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Sword" && other.gameObject.tag != "MeleeAttack" && isAlive )
        {
            attack = false;
            anticipation = false;
            isAttackable = true;
            DirectionArranger();
        }

        if (other.gameObject.tag == "Sword" && isAttackable == true)
        {
            DeathEvent();
        }
        
        

        
    }
    

    


}
