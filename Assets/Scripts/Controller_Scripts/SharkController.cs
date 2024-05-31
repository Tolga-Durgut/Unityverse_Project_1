using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    Collider2D col;
    [SerializeField] Collider2D playerCollider; // Player'ın collider'ı
    [SerializeField] private float jumpPower = 7f; 


    bool jump;
    bool isAlive = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        AnimationArranger();

    }




    void Jump()
    {
        
        rb.velocity = Vector2.up * jumpPower;
        
        
    }

    IEnumerator waitForJump()
    {
        yield return new WaitForSeconds(2f);
        jump = true;
        Jump();
    }

    void AnimationArranger()
    {
        
        animator.SetBool("Jump", jump);
        animator.SetFloat("yVelocity", rb.velocity.y);
        if (!isAlive)
        {

        animator.SetBool("IsAlive", false);
        }
        
        
    }

    void Death()
    {
        isAlive = false;
        Physics2D.IgnoreCollision(col, playerCollider, true);
        
    }
    

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "DownSword" && isAlive)
        {
            Death();
        }
        
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        
        if (other.gameObject.tag ==   "Ground" && isAlive) 
        {
            
            jump = false;
            StartCoroutine(waitForJump());
            
        }
    }
   
}
