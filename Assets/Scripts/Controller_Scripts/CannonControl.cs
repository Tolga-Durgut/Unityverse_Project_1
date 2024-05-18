using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
    Animator animator;
    bool fire;
    [SerializeField] GameObject cannonBall;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        AnimationArranger();
    }
    void AttackEvent()
    {
        StartCoroutine(waitForAttack());
    }

    IEnumerator waitForAttack()
    {
        yield return new WaitForSeconds(2f);
        fire = true;
    }

    void EndAttackEvent()
    {
        fire = false;
    }


    void AnimationArranger()
    {
        if (fire )
        {
            animator.SetBool("Fire",true);
            
        }
        else
        {
            animator.SetBool("Fire",false);
        }
    }

    void CannonBallSpawnerEvent()
    {
        Instantiate(cannonBall , transform.position , Quaternion.identity);
    }
}
