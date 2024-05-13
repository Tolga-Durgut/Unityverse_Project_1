using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabbyController : MonoBehaviour
{
    
    Animator animator;
    bool anticipation;
    bool attack;
   
    void Start()
    {
       animator = GetComponent<Animator>();
    }

    void Update()
    {

        AnimationArranger();
    }


    void StartAnticipation()
    {

       StartCoroutine(waitForAnticipation());

    }
    void StartAttack()
    {
        StartCoroutine(waitForAttack());
    }

    void AnimationArranger()
    {
        animator.SetBool("Anticipation" , anticipation);
        animator.SetBool("Attack" , attack);
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
    void EndAttackEvent()
    {
        anticipation = false;
        attack = false;
    }



}
