using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float cannonBallSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Move();

    }

    void Move()
    {
        rb.velocity = new Vector2 (-cannonBallSpeed * 100 * Time.fixedDeltaTime, rb.velocity.y);
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "Sword")
        {
            Destroy(this.gameObject);
            
        }
        
    }

}
