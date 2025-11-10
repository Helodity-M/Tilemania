using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    [SerializeField] Collider2D bounceCollider;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.linearVelocityX = MoveSpeed * transform.localScale.x;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

}
