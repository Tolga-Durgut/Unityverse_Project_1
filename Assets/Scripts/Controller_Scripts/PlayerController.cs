using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private SpriteRenderer playerSpriteRenderer;

    public SpriteRenderer PlayerSpriteRenderer
    {
        get { return playerSpriteRenderer; }
    }
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private float HorizontalMovementSpeed = 2f;
    [SerializeField] private float jumpPower = 500f;      
    [SerializeField] private Transform groundCheckCollider;
    [SerializeField] private Transform jumpDustPos;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] private float groundCheckColliderRatio = 0.1f;

    [SerializeField]private int availableJumps = 2;
    [SerializeField] bool IsGrounded = false;
    [SerializeField] GameObject sword;
    [SerializeField] float horizontalSpeed = 5f;
    private Animator playerAnimator;

    bool facingRight = true;
    float horizontalValue;

    bool melee = false;
    bool throwSword;
    bool jump = false;
    bool doubleJump = false;
    bool isAlive = true;

    [SerializeField] private GameObject dust;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
       
    }
    void Update()
    {
        if (isAlive)
        {

            GetInputs();
            DirectionArranger();
            AnimationArranger();

        }

        
        
        
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            GroundChecker();
            HorizontalMovement(horizontalValue);
            JumpMovement();
            
        }
        
    }

    private void GetInputs()
    {

        horizontalValue = Input.GetAxisRaw("Horizontal");
        //deneme scripti/////////
        if (Input.GetKey(KeyCode.H))
        {
            isAlive = false;
        }
        ////////////////////////
        if (Input.GetKey(KeyCode.E))
        {
            melee = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            melee = false;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            throwSword = true;
        }
       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
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

    private void HorizontalMovement(float dir)
    {
        float xVal = dir * horizontalSpeed *100* Time.fixedDeltaTime;
        Vector2 targetVelocity = new Vector2(xVal,rb.velocity.y);
        rb.velocity = targetVelocity;


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
                JumpDustAnim();
                
            }
            else if (!IsGrounded && availableJumps == 2)
            {
                rb.velocity = Vector2.up * jumpPower;
                doubleJump = false;
                availableJumps = 0;
                JumpDustAnim();
                
            }
            else if (doubleJump)
            {
                rb.velocity = Vector2.up * jumpPower;
                doubleJump = false;  
                availableJumps--;
                JumpDustAnim();


            }
            
            
            jump = false;
        }
    }

    private void JumpDustAnim()
    {
        
        Instantiate( dust, (Vector3) jumpDustPos.position,quaternion.identity);

    }
   
    public void ThrowSword()
    {
        if (throwSword )
        {
            Instantiate(sword,transform.position,quaternion.identity);
            
        }
        
    }
    public void EndThrowSword()
    {
        throwSword = false;
        
    }


    private void AnimationArranger()
    {
        playerAnimator.SetBool("IsAlive",isAlive);
        playerAnimator.SetBool("IsJumping", !IsGrounded);
        playerAnimator.SetFloat("yVelocity", rb.velocity.y);
    
        
        playerAnimator.SetBool("ThrowSword",throwSword);
        
        if (melee)
        {
            playerAnimator.SetBool("Melee", true);
            
        }
        else
        {
            playerAnimator.SetBool("Melee", false);
        }
        if (horizontalValue != 0)
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
        if (facingRight & horizontalValue < 0)
        {
            playerSpriteRenderer.flipX = true;
            facingRight = false;
           
        }
        else if (!facingRight & horizontalValue > 0)
        {
            playerSpriteRenderer.flipX = false;
            facingRight = true;
            
        }

        
    }

    private void Death()
    {
        isAlive = false;
    }
    
}
