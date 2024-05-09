using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    private Animator playerAnimator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float HorizontalMovementSpeed = 2f;
    [SerializeField] private float jumpPower = 500f;      
    [SerializeField] private Transform groundCheckCollider;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] private float groundCheckColliderRatio = 0.1f;

    
    [SerializeField]private int availableJumps = 2;
    bool IsGoingRight = false;
    bool IsGoingLeft = false;
    [SerializeField] bool IsGrounded = false;
    bool melee = false;
    bool jump = false;
    bool doubleJump = false;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
       
    }
    void Update()
    {
        GetInputs();
        DirectionArranger();
        AnimationArranger();
        
    }

    void FixedUpdate()
    {
        GroundChecker();
        HorizontalMovement();
        JumpMovement();
        
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
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

    private void GroundChecker()
    {
        IsGrounded = false;

        Collider2D[] collider2D = Physics2D.OverlapCircleAll(groundCheckCollider.position , groundCheckColliderRatio , groundLayer);

        if (collider2D.Length > 0 )
        {
            IsGrounded = true;

        }
    }

    private void HorizontalMovement()
    {

        if (IsGoingRight)
        {
            transform.Translate(HorizontalMovementSpeed * Time.fixedDeltaTime ,0, 0);
        }
        if (IsGoingLeft)
        {
            transform.Translate(-HorizontalMovementSpeed * Time.fixedDeltaTime,0, 0);
        }

    }

    private void JumpMovement()
    {
        if(!jump && IsGrounded)
        {
            availableJumps = 2;
        }
        else if (jump)
        {
            
            if (IsGrounded && availableJumps == 2)
            {
                rb.velocity = Vector2.up * jumpPower;
                doubleJump = true;
                availableJumps--;
                
            }
            else if (!IsGrounded && availableJumps == 2)
            {
                rb.velocity = Vector2.up * jumpPower;
                doubleJump = false;
                availableJumps = 0;
                
            }
            else if (doubleJump)
            {
                rb.velocity = Vector2.up * jumpPower;
                doubleJump = false;  
                availableJumps--;


            }
            
            
            jump = false;
        }
    }

    private void AnimationArranger()
    {
        
        playerAnimator.SetBool("IsJumping", !IsGrounded);
        playerAnimator.SetFloat("yVelocity", rb.velocity.y);
    

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
