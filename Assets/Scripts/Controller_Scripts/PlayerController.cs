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
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float jumpPower = 500f;      
    [SerializeField] private Transform groundCheckCollider;
    [SerializeField] private Transform jumpDustPos;

    [SerializeField] GameObject rightAttackObject;
    [SerializeField] GameObject leftAttackObject;
    [SerializeField] GameObject airAttackObject;
    [SerializeField] GameObject downSword;
   

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
    bool throwSword;
    bool downKey;

    public bool DownKey
    {
        get{ return downKey; }
    }
    bool jump = false;
    bool doubleJump = false;
    bool isAlive = true;

    public bool IsAlive
    {
        get{ return isAlive; }
    }

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
            MeleeAttack();

           
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
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    isAlive = true;
        //} 
        ////////


        horizontalValue = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.S))
        {
            downKey = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            downKey = false;
        }
        
        if (Input.GetKeyDown(KeyCode.J))
        { 
            
            melee = true;
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            melee = false;
        }

       
        if (Input.GetKeyDown(KeyCode.K) && isAlive)
        {
            throwSword = true;
        }
       

        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
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
        if(throwSword && downKey)
        {
            Instantiate(downSword,transform.position,quaternion.identity);
            throwSword = false;
        }
        else if (throwSword )
        {
            Instantiate(sword,transform.position,quaternion.identity);
            throwSword = false;
        }
        


        
    }
  
    private void MeleeAttack()
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

    private void AnimationArranger()
    {
        
        
        playerAnimator.SetBool("IsAlive",isAlive);
        playerAnimator.SetBool("IsJumping", !isGrounded);
        playerAnimator.SetFloat("yVelocity", rb.velocity.y);
        

        
        playerAnimator.SetBool( "ThrowSword",throwSword );
        
       

        
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

    private void Death(Vector3 DeathDirection)
    {
        isAlive = false;

        if (DeathDirection.x < transform.position.x)
        {
            rb.velocity = new Vector2(1,1) * jumpPower;
        }
        else
        {
            rb.velocity = new Vector2(-1,1) * jumpPower;
        }
        StartCoroutine(Spawn());

        

        
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.75f);
        transform.position = spawnPoint.position;
        isAlive = true;
    }


    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Enemy"  || other.gameObject.tag == "EnemyAttack")
        {

            Death(other.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if ( other.gameObject.tag == "EnemyAttack" )
        {
            Death(other.transform.position);
        }
    }

  
    
    
}
