using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField] private float cloudSpeed = 2f;
    [SerializeField]float leftTarget;
    [SerializeField]float rightTarget;

    // Update is called once per frame
    void Update()
    {
        Move();
        Parallax();
    }
    void Move()
    {
        transform.Translate(Vector2.left * Time.deltaTime * cloudSpeed);
    }
    void Parallax()
    {
       
  
        
        if (transform.position.x <=  leftTarget) 
        {
            float difference = transform.position.x - leftTarget  ;
            transform.position = new Vector2(rightTarget + difference, transform.position.y);
        }
        
    
    
    
    }

}
