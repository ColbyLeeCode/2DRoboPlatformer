using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    //width and height of the default capsule colliders
    Vector2 capsuleSize, capsuleOffset;
    //track if robot is sliding
    bool sliding = false;

    [SerializeField]
    GameObject healthCollider;

    //how fast the robot can move
    public float topSpeed = 10f;

    //get reference to animator
    Animator anim;

    public GameObject otherAnim;

    //not grounded
    bool grounded = false;

    //transform at robots feet to see if he is touching the ground
    public Transform groundCheck;
    //how big circle is when checking distance to ground
    float groundRadius = 0.2f;
     
    public float jumpForce = 3f;

    //what layer is considered ground
    public LayerMask whatIsGround;

    //var to check double jump
    bool doubleJump = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        //sanity check to ensure on start player isn't dead
        anim.SetBool("isDead", false);

        //get the default sizes of our capsules being used store in vector
        float capsuleWidth = healthCollider.GetComponent<CapsuleCollider2D>().size.x;
        float capsuleHeight = healthCollider.GetComponent<CapsuleCollider2D>().size.y;
        //get the default offset of our capsule colliders store in vector
        float offsetX = healthCollider.GetComponent<CapsuleCollider2D>().offset.x;
        float offsetY = healthCollider.GetComponent<CapsuleCollider2D>().offset.y;

        capsuleSize = new Vector2(capsuleWidth, capsuleHeight);
        capsuleOffset = new Vector2(offsetX, offsetY);
    }
    //physics in fixed update
    void FixedUpdate()
    {
        //bool did ground transform hit whatIsGround given radius
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        //tell the animator that we are grounded
        anim.SetBool("Ground", grounded);

        //reset double jump
        if (grounded)
            doubleJump = false;

        //get verticle speed from rigidbody
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        //get move direction if player is not sliding
        if (!sliding)
        {
            float move = Input.GetAxis("Horizontal");

            //add velocity to the rigidbody in the move direction * our speed
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * topSpeed, GetComponent<Rigidbody2D>().velocity.y);

            //set speed parameter to our horizontal movement speed to animate running
            anim.SetFloat("Speed", Mathf.Abs(move));

            if (move > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;

            }
            else if (move < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;

            }
        }
    }

    void Update()
    {
        GetInputMovement();
        
    }

    private void GetInputMovement()
    {
        //-----Handle Jumping-----

        //jump if not already double jumped and on the ground
        if ((grounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            //not on the ground
            anim.SetBool("Ground", false);
            //set the speed of y axis  to zero prior to adding additonal force
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            //add jump force the Y axis of the rigid body of the robot
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            //not sliding
            sliding = false;

            if (!doubleJump && !grounded)
                doubleJump = true;
        }

        //-----Handle Sliding-----

        //if slide is pressed and not already sliding or in the air, set sliding to true
        if (Input.GetButtonDown("Slide") && !sliding && grounded)
        {           
            anim.SetBool("isSliding", true);

            sliding = true;

            //resize healthcollider and groundCheck capsule smaller for sliding under things            
            healthCollider.GetComponent<CapsuleCollider2D>().size = new Vector2(capsuleSize.x, capsuleSize.y/ 2);
            healthCollider.GetComponent<CapsuleCollider2D>().offset = new Vector2(capsuleOffset.x, -1.26f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(capsuleSize.x, capsuleSize.y / 2);
            GetComponent<CapsuleCollider2D>().offset = new Vector2(capsuleOffset.x, -1.26f);

        }

        //if sliding velocity has stopped event has ended set sliding, to false
        float xVelocity = GetComponent<Rigidbody2D>().velocity.x;
        if (Mathf.Abs(xVelocity) < 0.2f)
        {
            anim.SetBool("isSliding", false);

            sliding = false;
        }

        if(!sliding)
        {
            //resize healthcollider and groundCheck capsule back to full size
            healthCollider.GetComponent<CapsuleCollider2D>().size = capsuleSize;
            healthCollider.GetComponent<CapsuleCollider2D>().offset = capsuleOffset;
            GetComponent<CapsuleCollider2D>().size = capsuleSize;
            GetComponent<CapsuleCollider2D>().offset = capsuleOffset;
        }

        //if not sliding ensure capsule is correct size
    }
}
