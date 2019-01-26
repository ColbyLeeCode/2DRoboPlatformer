using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float damage = 33.3f;
    
    private RobotController player;
    private GameManager manager;
    private Rigidbody2D body;
    private AudioManager audio;

    void Start()
    {
        player = FindObjectOfType<RobotController>();
        manager = FindObjectOfType<GameManager>();
        body = GetComponent<Rigidbody2D>();
        audio = FindObjectOfType<AudioManager>();

        if (player.transform.localScale.x < 0)
        {
            bulletSpeed = -bulletSpeed;
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

       
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.name != "Robot")
        {
            Debug.Log(collision.collider.name);
            audio.PlaySound("electrictick");
            Destroy(gameObject);
        }
       
    }

    void Update()
    {
        body.velocity = new Vector2(bulletSpeed, GetComponent<Rigidbody2D>().velocity.y);

        
        Destroy(gameObject, .5f);
    }
}
