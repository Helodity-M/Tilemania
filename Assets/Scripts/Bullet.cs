using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float MoveSpeed;

    Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.linearVelocityX = MoveSpeed * Mathf.Sign(transform.localScale.x);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
