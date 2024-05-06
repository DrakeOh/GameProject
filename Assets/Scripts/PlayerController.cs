    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PlayerController : MonoBehaviour
    {


        private bool isCollidingWithEnemy = false;
        private float collisionTimer = 0f;

    public int maxHealth = 100;
    public int currentHealth; 
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
        public Health popUpMessage;
        public  PauseMenu GameOver;

    // Start is called before the first frame update
    void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        currentHealth = maxHealth;


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
                    playerAttack.transform.SetLocalPositionAndRotation(new Vector3(0.16f, 0, 0), Quaternion.identity);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    animator.SetBool("WalkLeft", true);
                    // Flip the character sprite when walking left
                    transform.localScale = new Vector3(-1, 1, 1);
                    playerAttack.transform.SetLocalPositionAndRotation(new Vector3(0.16f, 0, 0), Quaternion.identity);
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    animator.SetBool("WalkForward", true);
                    // Set the facing direction to forward
                    playerAttack.transform.SetLocalPositionAndRotation(new Vector3(0, 0.16f, 0), Quaternion.Euler(0, 0, 90));
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    animator.SetBool("WalkBackward", true);
                    playerAttack.transform.SetLocalPositionAndRotation(new Vector3(0, -0.16f, 0), Quaternion.Euler(0, 0, -90));
                }
            }
        }

    private void HandleAttack()
    {
        if (Input.GetKey(KeyCode.K))
        {
            isAttacking = true;
            // Set the trigger for the sword attack animation
            animator.SetTrigger("SwordAttack");

            Attack(facingDirection);
        }
    }

    private void Attack(Vector3 direction)
    {
        // Cast a ray in the direction of the attack to detect enemies
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 1f);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                // Get the Enemy component from the collider and apply damage
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(20); // Adjust damage as needed
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);
        popUpMessage.ShowMessage("health : " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        GetComponent<Collider2D>().enabled = false;
        Time.timeScale = 0;
        Debug.Log("Player died");
        GameOver.gameOver();

    }

}