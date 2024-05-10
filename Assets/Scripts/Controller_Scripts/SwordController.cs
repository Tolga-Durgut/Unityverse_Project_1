using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SwordController : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController playerController;
    Rigidbody2D playerRb;
    GameObject player;
    bool movementFlag = true;
    [SerializeField] private float throwForce = 7;



    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();   
        playerController = player.GetComponent<PlayerController>();
        playerRb = player.GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        SwordMovement();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag != "Player"   && other.gameObject.tag != "Sword")
        {
            Destroy(this.gameObject);
        }
    }

    private void SwordMovement()
    {
        if (movementFlag)
        {

            movementFlag = false;
            if (playerController.SwordControllerSwordDirection == true)
            {
                
                rb.velocity = Vector2.left * (throwForce + playerRb.velocity.x) ;
            }
            else if (playerController.SwordControllerSwordDirection == false)
            {
                rb.velocity = Vector2.right * (throwForce + playerRb.velocity.x) ;
            }

            Debug.Log(throwForce);
            Debug.Log(playerRb.velocity.x);

        }
    }


    
}
