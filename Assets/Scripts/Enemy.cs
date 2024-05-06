using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    private Animator animator;
    public float speed = 5f; // Adjust the speed as needed
    private PlayerController player; // Reference to the player's transform
    public AudioSource audioSource;
    public bool dead;
     [SerializeField] float damage;
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); // Find the player object
        if (playerObject != null)
        {
            player = playerObject.GetComponent<PlayerController>(); // Get the PlayerController component
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    void Update()
    {
        // Check if the player exists
        if (player != null)
        {
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       
        // Check if collided with the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Die when colliding with the player
            Die();
            if (player != null)
            {
                // Apply damage to the player
                player.TakeDamage(100); // Adjust damage as needed
            }


        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            dead = true;
        }
        
    }
    public void PlaySound()
    {
        if (audioSource != null)
        {
            if (dead == true)
            
                audioSource.Play();
            
            
        }
    }
    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2f);
        Debug.Log("Enemy died");
    }

    public static implicit operator Enemy(bool v)
    {
        throw new NotImplementedException();
    }
}
