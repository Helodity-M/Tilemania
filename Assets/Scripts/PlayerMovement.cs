using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector2 moveInput;
    Animator animator;
    Collider2D collider;

    float baseGravityScale;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        baseGravityScale = rb2d.gravityScale;
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        Run();
        ClimbLadder();
        UpdateAnimation();
    }
    void Run()
    {
        rb2d.linearVelocityX = moveInput.x * moveSpeed;
    }


    void UpdateAnimation()
    {
        animator.SetBool("IsRunning", moveInput.x != 0);
        animator.SetBool("IsClimbing", collider.IsTouchingLayers(LayerMask.GetMask("Climbable")));
        if(moveInput.x != 0) //Only flip if moving
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x),1,1);
    }

    void ClimbLadder()
    {
        //If we are on a ladder, include y movement.
        if (collider.IsTouchingLayers(LayerMask.GetMask("Climbable")))
        {
            rb2d.linearVelocityY = moveInput.y * moveSpeed;
            rb2d.gravityScale = 0;
        } else
        {
            rb2d.gravityScale = baseGravityScale;
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            if(collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                rb2d.linearVelocityY = jumpForce;
            }
        } else
        {
            if(rb2d.linearVelocityY > 0)
            {
                rb2d.linearVelocityY *= 0.2f;
            }
        }
    }
}
