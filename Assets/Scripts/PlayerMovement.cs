using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector2 moveInput;
    Animator animator;

    [SerializeField] float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Run();
    }
    void Run()
    {
        rb2d.linearVelocityX = moveInput.x * moveSpeed;
        animator.SetBool("IsRunning", moveInput.x != 0);
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
