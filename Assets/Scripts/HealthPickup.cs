using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    PlayerHealth playerHealth;
    AudioManager audio;

    public float healthBonus = 15f;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        audio = FindObjectOfType<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //check if player collides with healthpickup
        playerHealth.currHealth += healthBonus;
        audio.PlaySound("bing");
        Destroy(gameObject);       
    }
}
