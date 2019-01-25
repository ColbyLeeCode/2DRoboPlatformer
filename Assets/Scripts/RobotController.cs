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

    public Transform muzzle;

    public GameObject bullet;

    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;

        anim = GetComponent<Animator>();
        //sanity check to ensure on start player isn't dead
        anim.SetBool("isDead", false);

        //get the default sizes of our capsules being used 
        float dCapsuleWidth = healthCollider.GetComponent<CapsuleCollider2D>().size.x;
        float dCapsuleHeight = healthCollider.GetComponent<CapsuleCollider2D>().size.y;
        //get the default offset of our capsule colliders
        float offsetX = healthCollider.GetComponent<CapsuleCollider2D>().offset.x;
        float offsetY = healthCollider.GetComponent<CapsuleCollider2D>().offset.y;

        capsuleSize = new Vector2(dCapsuleWidth, dCapsuleHeight);
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
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (move < 0)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
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
            sliding = false;
            //not on the ground
            anim.SetBool("Ground", false);
            //set the speed of y axis  to zero prior to adding additonal force
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            //add jump force the Y axis of the rigid body of the robot
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            //not sliding
            

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
            CapsuleResize(healthCollider.GetComponent<CapsuleCollider2D>(), false);
            CapsuleResize(GetComponent<CapsuleCollider2D>(), false);
        }

        //if sliding velocity has stopped event has ended set sliding, to false       
        float xVelocity = GetComponent<Rigidbody2D>().velocity.x;
        if (Mathf.Abs(xVelocity) < 0.2f || !grounded)
        {
            anim.SetBool("isSliding", false);

            sliding = false;
        }
   

        if(!sliding)
        {
            //resize healthcollider and groundCheck capsule back to full size
            CapsuleResize(healthCollider.GetComponent<CapsuleCollider2D>(), true);
            CapsuleResize(GetComponent<CapsuleCollider2D>(), true);
        }

        //-----Handle Firing Weapon-----
        if (Input.GetButtonDown("Fire1") && !sliding)
        {

            GameObject nBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);

            audioManager.PlaySound("firebullet");

            nBullet.transform.parent = GameObject.Find("GameManager").transform;

            nBullet.GetComponent<Renderer>().sortingLayerName = "Player";

            anim.SetBool("isShooting", true);
        }

        if(Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("isShooting", false);
            anim.SetBool("isRunningShot", false);
        }

        if(Input.GetButtonDown("Fire1") && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 0)
        {
            anim.SetBool("isRunningShot", true);
        }
    }

    //Resizes and offsets the capsule for sliding - shrinks the capsule or restores to default sizes and offset
    private void CapsuleResize(CapsuleCollider2D capsule, bool defaults)
    {
        //reset to default size (running / jumping)
        if(defaults)
        {
            capsule.size = capsuleSize;
            capsule.offset = capsuleOffset;
        }
        //resizes capsule for sliding
        else
        {
            capsule.size = new Vector2(capsuleSize.x, capsuleSize.y / 1.35f);
            if (GetComponent<SpriteRenderer>().flipX)
                capsule.offset = new Vector2(1.39f, -0.73f);
            else
                capsule.offset = new Vector2(-1.39f, -0.73f);
        }
    }
}
