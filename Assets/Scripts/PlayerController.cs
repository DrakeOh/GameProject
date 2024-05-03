using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;


    Vector2 movementInput;

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

        HandleAnimation();

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




    // private void IsMoving()
    //  {
    //   if
    // }
    private void HandleAnimation()
    {
         if(TryMove(movementInput) == true) ;
        { 
            // Reset all animation triggers and set all boolean parameters to false
            animator.ResetTrigger("WalkRight");
            animator.ResetTrigger("WalkLeft");
            animator.ResetTrigger("WalkForward");
            animator.ResetTrigger("WalkBackward");
            animator.SetBool("IsWalkingRight", false);
            animator.SetBool("IsWalkingLeft", false);
            animator.SetBool("IsWalkingForward", false);
            animator.SetBool("IsWalkingBackward", false);

            // Check movement keys and set the appropriate animation triggers and boolean parameters
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("WalkRight", true);
                // Reset scale to normal if previously flipped
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("WalkLeft", true);
                // Flip the character sprite when walking left
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("WalkForward", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("WalkBackward", true);
            }
        }
    }





}