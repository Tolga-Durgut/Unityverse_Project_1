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
    
    [SerializeField] private float jumpPower = 500f;      
    [SerializeField] private Transform groundCheckCollider;
    [SerializeField] private Transform jumpDustPos;

    [SerializeField] GameObject rightAttackObject;
    [SerializeField] GameObject leftAttackObject;
   

    [SerializeField] LayerMask groundLayer;
    [SerializeField] private float groundCheckColliderRatio = 0.1f;

    [SerializeField]private int availableJumps = 2;
    [SerializeField] bool isGrounded = false;

    public bool IsGrounded
    {
        get { return IsGrounded; }
    }
    [SerializeField] GameObject sword;
    [SerializeField] float horizontalSpeed = 5f;
    private Animator playerAnimator;
    

    bool facingRight = true;

    public bool FacingRight
    {
        get{ return facingRight; }
    }

    float horizontalValue;

    public float HorizontalValue
    {
        get{ return horizontalValue; }
    }

    bool melee = false;
    bool airAttack = false;
    bool throwSword;
    bool jump = false;
    bool doubleJump = false;
    bool isAlive = true;

    [SerializeField]bool makeDust;


    [SerializeField] private GameObject dust;
    [SerializeField] private GameObject land;
    [SerializeField] private GameObject walkDust;
    
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
       
    }
    void Update()
    {
            GetInputs();
        if (isAlive)
        {

            DirectionArranger();
            Attack();
           
        }
            AnimationArranger();

        
        
    }

    void FixedUpdate()
    {
            GroundChecker();
        if (isAlive)
        {
            HorizontalMovement(horizontalValue);
            JumpMovement();
            LandDustAnim();
            
           
            
        }
        
    }

    private void GetInputs()
    {


        ///////// Test Scripti 
        if (Input.GetKeyDown(KeyCode.R))
        {
            isAlive = true;
        } 
        ////////


        horizontalValue = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.K))
        { 
            
            melee = true;
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            melee = false;
        }

        if (Input.GetKeyDown(KeyCode.I) && !isGrounded)
        {
            airAttack = true;
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            airAttack = false;
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
        isGrounded = false;

        Collider2D[] collider2D = Physics2D.OverlapCircleAll(groundCheckCollider.position , groundCheckColliderRatio , groundLayer);

        if (collider2D.Length > 0 )
        {
            isGrounded = true;

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
        if(!jump && isGrounded)
        {
            availableJumps = 2;
        }
        else if (jump)
        {
            
            if (isGrounded && availableJumps == 2)
            {
                rb.velocity = Vector2.up * jumpPower;
                doubleJump = true;
                availableJumps--;
                JumpDustAnim();
                
            }
            else if (!isGrounded && availableJumps == 2)
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

    private void LandDustAnim()
    {
        if (rb.velocity.y < -7)
        {
            makeDust = true;
        }
        if (rb.velocity.y == 0 && isGrounded && makeDust)
        {   
            
            Instantiate(land , transform.position  , quaternion.identity);
            makeDust = false;
            
        }
        
    }
    
    public void ThrowSwordEvent()
    {
        if (throwSword )
        {
            Instantiate(sword,transform.position,quaternion.identity);
            throwSword = false;
        }
        
    }
  
    private void Attack()
    {
        if (melee && isGrounded && horizontalValue == 0)
        {
            if (facingRight)
            {
                rightAttackObject.SetActive(true);
                
            }
            else if (!facingRight)
            {
                leftAttackObject.SetActive(true);

            }
            
            
        }

    }
    
    public void AirAttackCheckerEvent()
    {
        airAttack = false;
        

    }
    private void AnimationArranger()
    {
        
        
        playerAnimator.SetBool("IsAlive",isAlive);
        playerAnimator.SetBool("IsJumping", !isGrounded);
        playerAnimator.SetFloat("yVelocity", rb.velocity.y);


        
        playerAnimator.SetBool( "ThrowSword",throwSword );
        
        if ( !isGrounded && airAttack  )
        {

            playerAnimator.SetBool("AirAttack", true);
            

        }
        if( isGrounded || !airAttack )
        {
            airAttack = false;
            playerAnimator.SetBool("AirAttack", false);
        }
     

        
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

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            Death();
        }
    }

  
    
    
}
