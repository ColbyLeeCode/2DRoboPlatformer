using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    Text healthText;
    [SerializeField]
    GameObject DeathUI;
    [SerializeField]
    GameObject Robot;

    Animator anim;

    float maxHealth = 100;
    float currHealth;

    void Start()
    {
        anim = Robot.GetComponent<Animator>();
        healthBar.value = maxHealth;
        currHealth = healthBar.value;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Saw")
        {
            healthBar.value -= 1;
            currHealth = healthBar.value;
        }

        if(col.gameObject.tag == "Acid")
        {
            healthBar.value -= .1f;
            currHealth = healthBar.value;
        }
    }

    void Update()
    {
        healthText.text = currHealth.ToString("n0") + " %";

        if (currHealth <= 0)
        {
            anim.SetBool("isDead", true);
            Robot.GetComponent<RobotController>().enabled = false;

            DeathUI.gameObject.SetActive(true);
        }

        
    }
}
