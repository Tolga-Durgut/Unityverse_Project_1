using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabbyController : MonoBehaviour
{
    
    Animator animator;
    bool anticipation;
    bool attack;
    bool damage = false;
    bool isAlive = true;
    [SerializeField] GameObject crabbyAttackAnimObject;
    Collider2D col;

    int health = 4;
   
    void Start()
    {
       animator = GetComponent<Animator>();
       col = GetComponent<Collider2D>();
    }

    void Update()
    {
        AnimationArranger();
        Death();
    }

    #region AttackCycleCode
    void StartAnticipation()
    {

       StartCoroutine(waitForAnticipation());

    }
    void StartAttack()
    {
        StartCoroutine(waitForAttack());
    }
    IEnumerator waitForAnticipation()
    {
        yield return new WaitForSeconds(0.5f);
        anticipation= true;
    }
    IEnumerator waitForAttack()
    {
        yield return new WaitForSeconds(0.5f);
        attack = true;
    }

    public void AttackAnimEvent()
    {
        crabbyAttackAnimObject.SetActive(true);
    }

    void EndAttackEvent()
    {
        anticipation = false;
        attack = false;
        crabbyAttackAnimObject.SetActive(false);
    }
    #endregion 

    public void TakeDamageEvent()
    {
        damage = false;
        health --;
        
    }
    void Death()
    {
        if (health == 0)
        {
            isAlive = false;
            col.enabled = false;

        }
    }


    void AnimationArranger()
    {
        if (!isAlive)
        {
            animator.SetBool("IsAlive",false);
        }
        animator.SetBool("Anticipation" , anticipation);
        animator.SetBool("Attack" , attack);
        animator.SetBool("Damage" , damage);
    }



    void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (other.gameObject.tag == "MeleeAttack")
        {
            damage = true;
        }
    }
   



}
