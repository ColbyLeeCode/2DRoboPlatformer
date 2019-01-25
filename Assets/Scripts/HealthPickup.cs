using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    PlayerHealth playerHealth;

    public float healthBonus = 15f;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //check if player collides with healthpickup
        playerHealth.currHealth += healthBonus;
        Destroy(gameObject);       
    }
}
