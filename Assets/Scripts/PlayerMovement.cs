using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector2 moveInput;
    Animator animator;
    SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D bodyCollider;
    [SerializeField] Collider2D floorCollider;

    float baseGravityScale;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    [SerializeField] LayerMask JumpableLayers;
    [SerializeField] LayerMask ClimbableLayers;
    [SerializeField] LayerMask DeathLayers;

    bool isAlive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        baseGravityScale = rb2d.gravityScale;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isAlive) return;
        Run();
        ClimbLadder();
        UpdateAnimation();
        Die();
    }
    void Run()
    {
        rb2d.linearVelocityX = moveInput.x * moveSpeed;
    }


    void UpdateAnimation()
    {
        animator.SetBool("IsRunning", moveInput.x != 0);
        animator.SetBool("IsClimbing", floorCollider.IsTouchingLayers(ClimbableLayers));
        if(moveInput.x != 0) //Only flip if moving
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x),1,1);
    }

    void ClimbLadder()
    {
        //If we are on a ladder, include y movement.
        if (bodyCollider.IsTouchingLayers(ClimbableLayers))
        {
            rb2d.linearVelocityY = moveInput.y * moveSpeed;
            rb2d.gravityScale = 0;
        } else
        {
            rb2d.gravityScale = baseGravityScale;
        }
    }

    void Die()
    {
        if (bodyCollider.IsTouchingLayers(DeathLayers))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            rb2d.linearVelocityY += 30;
            spriteRenderer.color = Color.red;
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) return;
        if (value.isPressed)
        {
            if(floorCollider.IsTouchingLayers(JumpableLayers))
            {
                rb2d.linearVelocityY = jumpForce;
            }
        } else
        {
            if(rb2d.linearVelocityY > 0)
            {
                rb2d.linearVelocityY *= 0.5f;
            }
        }
    }
}
