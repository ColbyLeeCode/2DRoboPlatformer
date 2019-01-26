using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    AudioManager audio;

    public float health = 100f;

    public Transform target;

    public float engageDistance = 10f;

    public float attackDistance = 3f;

    public float moveSpeed = 5f;

    private bool facingLeft = false;

    private float gurgleTime = 3f;
    private float timer = 4f;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = FindObjectOfType<AudioManager>();

        //set collider used when dead to false
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update()
    {
        //make gurgle noises when appropriate
        if(!anim.GetBool("isDead"))
            Gurgle();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //if not dead
        if (health > 0)
        {
            //set all animations to false
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isIdle", false);

            if (Vector3.Distance(target.position, this.transform.position) < engageDistance)
            {
                //get the direction of the target
                Vector3 direction = target.position - this.transform.position;               

                if (Mathf.Sign(direction.x) == 1 && facingLeft)
                {
                    Flip();
                }
                else if (Mathf.Sign(direction.x) == -1 && !facingLeft)
                {
                    Flip();
                }

                if (direction.magnitude >= attackDistance)
                {
                    Debug.DrawLine(target.transform.position, this.transform.position, Color.yellow);

                    
                    if (facingLeft)
                    {

                        //this.transform.Translate(new Vector2(Time.deltaTime * moveSpeed, this.transform.position.y));
                        GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                        anim.SetBool("isWalking", true);

                    }
                    else if (!facingLeft)
                    {
                        //add velocity to the rigidbody in the move direction * our speed
                        GetComponent<Rigidbody2D>().velocity = new Vector2(1 * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                        anim.SetBool("isWalking", true);
                    }
                }
                if (direction.magnitude < attackDistance)
                {
                    Debug.DrawLine(target.transform.position, this.transform.position, Color.red);
                    anim.SetBool("isAttacking", true);
                    audio.PlaySound("zombiegurgle");

                }

            }
            else if (Vector3.Distance(target.position, this.transform.position) > engageDistance)
            {
                //do nothing
                Debug.DrawLine(target.position, this.transform.position, Color.green);
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isIdle", true);

            }

            
        }

        //just died
        else if(!anim.GetBool("isDead"))
        {
            anim.SetBool("isDead", true);
            //remove capsule collider, replace with flat collider for dead zombies
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }


    void Gurgle()
    {
        //gurgle every three seconds
        if (anim.GetBool("isWalking"))
        {
            timer += Time.deltaTime;
            if (timer > gurgleTime)
            {
                audio.PlaySound("zombiegurgle");
                timer = 0f;
            }
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        //Find out what hit the zombie
        if(col.gameObject.tag == "Bullet")
        {
            health -= col.gameObject.GetComponent<Bullet>().damage;
            Debug.Log("Zombie health: " + health);
        }    
    }

    private void Flip()
    {
        facingLeft = !facingLeft;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;
    }
}
