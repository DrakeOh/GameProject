using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;
    private Vector3 facingDirection = Vector3.forward;

    Vector2 movementInput;
    private bool isAttacking = false;
    public bool sucsess;
    Rigidbody2D rb;
    private Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {

             sucsess = TryMove(movementInput);

            if (!sucsess)
            {
                sucsess = TryMove(new Vector2(movementInput.x, 0));

                if (!sucsess)
                {
                    sucsess = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }

        HandleMovement();
        HandleAttack();
       
        //if (Input.GetKeyDown(KeyCode.D))
        //{

        //    animator.SetBool("IsMoving", true);
        //    animator.SetTrigger("WalkRight");


        //}
        //else
        //{

        //    if (animator != null)
        //    {
        //        animator.SetBool("IsMoving", false);
        //    }
        //}

    }
    private bool TryMove(Vector2 direction)
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
                // Set the facing direction to right
                facingDirection = Vector3.right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("WalkLeft", true);
                // Flip the character sprite when walking left
                transform.localScale = new Vector3(-1, 1, 1);
                // Set the facing direction to left
                facingDirection = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("WalkForward", true);
                // Set the facing direction to forward
                facingDirection = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("WalkBackward", true);
                // Set the facing direction to backward
                facingDirection = Vector3.back;
            }
        }
    }

    private void HandleAttack()
    {
        // Check if the left mouse button (button 0) is pressed for attack
        if (Input.GetMouseButtonDown(0))
        {
            // Set the trigger for the sword attack animation
            animator.SetTrigger("SwordAttack");

            // Pass the facing direction to the Attack method (assuming you have an Attack method to handle the attack logic)
            Attack(facingDirection);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void Attack(Vector3 direction)
    {
        // Add your attack logic here
        // You can use the 'direction' vector to determine the direction of the attack
    }


}