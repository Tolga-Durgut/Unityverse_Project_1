using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float HorizontalMovementSpeed = 2f;    
    bool IsGoingRight = false;
    bool IsGoingLeft = false;
    bool melee = false;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        GetInputs();
        DirectionArranger();
        AnimationArranger();
        HorizontalMovement();
    }

    void FixedUpdate()
    {
       // HorizontalMovement();
        
    }

    private void GetInputs()
    {
        
        if (Input.GetKey(KeyCode.E))
        {
            melee = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            melee = false;
        }
        if (Input.GetKeyDown(KeyCode.A)  || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            IsGoingLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            IsGoingLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            IsGoingRight = true;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            IsGoingRight = false;
        }

    }

    private void HorizontalMovement()
    {

        if (IsGoingRight)
        {
            transform.Translate(HorizontalMovementSpeed * Time.deltaTime ,0, 0);
        }
        if (IsGoingLeft)
        {
            transform.Translate(-HorizontalMovementSpeed * Time.deltaTime,0, 0);
        }

    }

    private void AnimationArranger()
    {
        if (melee)
        {
            playerAnimator.SetBool("Melee", true);
            
        }
        else
        {
            playerAnimator.SetBool("Melee", false);
        }
        if (IsGoingLeft || IsGoingRight)
        {
            playerAnimator.SetBool("IsRunning", true);
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
        }
    }
    
    private void DirectionArranger()
    {
        if (IsGoingRight & playerSpriteRenderer.flipX == true)
        {
            playerSpriteRenderer.flipX = false;
        }
        if (IsGoingLeft & playerSpriteRenderer.flipX == false)
        {
            playerSpriteRenderer.flipX = true;
        }
    }
    
}
