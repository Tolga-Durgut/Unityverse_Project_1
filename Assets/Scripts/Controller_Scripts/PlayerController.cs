using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float HorizontalMovementSpeed = 2f;    
    bool IsGoingRight = false;
    bool IsGoingLeft = false;

    void Update()
    {
        GetInputs();
    }

    void FixedUpdate()
    {
        HorizontalMovement();
    }

    private void GetInputs()
    {
        /*
        if (Input.GetAxis("Horizontal") > 0)
        {
            IsGoingRight = true;
            IsGoingLeft = false;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            IsGoingLeft = true;
            IsGoingRight = false;
        }

        if (Input.GetAxis("Horizontal") == 0 )
        {
            IsGoingLeft = false;
            IsGoingRight = false;
        }
        */


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
            transform.Translate(HorizontalMovementSpeed * Time.fixedDeltaTime ,0, 0);
        }
        if (IsGoingLeft)
        {
            transform.Translate(-HorizontalMovementSpeed * Time.fixedDeltaTime,0, 0);
        }

    }
}
