using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabbyController : MonoBehaviour
{
    
    Animator animator;
    PlayerController playerController;
    bool anticipation;
    bool attack;
    bool isAlive = true;
    bool isAttackable = true;
    [SerializeField] GameObject crabbyAttackAnimObject;
    Collider2D col;


   
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
       animator = GetComponent<Animator>();
       col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if(!isAlive && !playerController.IsAlive)
        {
            StartCoroutine(waitForReborn());
            
        }
        AnimationArranger();
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
        isAttackable = false;
        crabbyAttackAnimObject.SetActive(true);
    }

    void EndAttackEvent()
    {
        anticipation = false;
        attack = false;
        isAttackable = true;
        crabbyAttackAnimObject.SetActive(false);
    }
    #endregion 

   


    void AnimationArranger()
    {
        
        animator.SetBool("IsAlive",isAlive);
        
        animator.SetBool("Anticipation" , anticipation);
        animator.SetBool("Attack" , attack);
    }

    void Death()
    {
        isAlive = false;
        col.enabled = false;
    }

    void Reborn()
    {
        attack = false;
        anticipation = false;
        col.enabled = true;
        isAlive = true;

    }

    IEnumerator waitForReborn()
    {
        yield return new WaitForSeconds(0.75f);
        Reborn();
    }



    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "MeleeAttack" && isAttackable)
        {
            Death();
        }
        
    }
   



}
