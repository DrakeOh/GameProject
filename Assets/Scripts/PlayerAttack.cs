using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    Collider2D swordCollider;
    Transform swordTransform;
    public PlayerController playerController;
    public int damage = 10; // Amount of damage the sword deals

    private void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        swordTransform = transform;
    }

    // Update the collider's position and rotation in FixedUpdate
    private IEnumerator EnableColliderForDuration(float duration)
    {
        float seconds = 0.7f;
        swordCollider.enabled = true;
        playerController.isAttacking = true;
        yield return new WaitForSeconds(seconds);
        swordCollider.enabled = false;
        playerController.isAttacking = false;
    }

    private void FixedUpdate()
    {
        if (playerController.isAttacking)
        {
            StartCoroutine(EnableColliderForDuration(3f)); // Enable collider for 3 seconds
        }
    }

    public void UpdateDirection(Vector3 direction)
    {
        // Set the rotation of the sword collider to match the direction
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with an enemy
        if (other.CompareTag("enemy"))
        {
            // Handle collision with enemy
            Debug.Log("Collision detected with an enemy: " + other.gameObject.name);

            // Apply damage to the enemy
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
