using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isCollidingWithEnemy = false;
    private float collisionTimer = 0f;
    public float collisionDuration = 1f; // Adjust the collision duration as needed

    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public bool isAttacking = false;
    public bool success;

    public ContactFilter2D movementFilter;
    private Vector3 facingDirection = Vector3.forward;
    Vector2 movementInput;

    Rigidbody2D rb;
    private Animator animator;
    public SwordAttack playerAttack;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Set current health to maximum health when the game starts
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }

        HandleMovement();
        HandleAttack();
    }

    private bool TryMove(Vector2 direction)
    {
        if (!isAttacking)
        {
            int count = rb.Cast(
                   direction,
                   movementFilter,
                   castCollisions,
                  moveSpeed * Time.fixedDeltaTime + collisionOffset);
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }

        }
        return false;
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private void HandleMovement()
    {
        if (!isAttacking)
        {
            // Reset all animation triggers and set all boolean parameters to false
            animator.ResetTrigger("WalkRight");
            animator.ResetTrigger("WalkLeft");
            animator.ResetTrigger("WalkForward");
            animator.ResetTrigger("WalkBackward");

            // Reset all boolean parameters to false
            animator.SetBool("WalkRight", false);
            animator.SetBool("WalkLeft", false);
            animator.SetBool("WalkForward", false);
            animator.SetBool("WalkBackward", false);

            // Check movement keys and set the appropriate animation triggers and boolean parameters
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("WalkRight", true);
                // Reset scale to normal if previously flipped
                transform.localScale = new Vector3(1, 1, 1);
                playerAttack.transform.localPosition = new Vector3(0.16f, 0, 0);
                playerAttack.transform.localRotation = Quaternion.identity;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("WalkLeft", true);
                // Flip the character sprite when walking left
                transform.localScale = new Vector3(-1, 1, 1);
                playerAttack.transform.localPosition = new Vector3(0.16f, 0, 0);
                playerAttack.transform.localRotation = Quaternion.identity;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("WalkForward", true);
                // Set the facing direction to forward
                playerAttack.transform.localPosition = new Vector3(0, 0.16f, 0);
                playerAttack.transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("WalkBackward", true);
                playerAttack.transform.localPosition = new Vector3(0, -0.16f, 0);
                playerAttack.transform.localRotation = Quaternion.Euler(0, 0, -90);
            }
        }
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            // Set the trigger for the sword attack animation
            animator.SetTrigger("SwordAttack");

            Attack(facingDirection);
        }
    }

    private void Attack(Vector3 direction)
    {
        // Add your attack logic here
        // You can use the 'direction' vector to determine the direction of the attack
        // For example, you can cast a ray or check for collision with enemies here
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add death logic here, such as playing death animation, resetting the game, etc.
        Debug.Log("Player died!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Set collision flag to true when colliding with the enemy
            isCollidingWithEnemy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Reset collision flag and timer when no longer colliding with the enemy
            isCollidingWithEnemy = false;
            collisionTimer = 0f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // If the collision is with the enemy, start the collision timer
            isCollidingWithEnemy = true;
            collisionTimer += Time.deltaTime;

            // If the collision duration has been reached, apply damage to the player
            if (collisionTimer >= collisionDuration)
            {
                TakeDamage(10); // Example: Player takes 10 damage
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Reset collision flag and timer when no longer colliding with the enemy
            isCollidingWithEnemy = false;
            collisionTimer = 0f;
        }
    }
}
