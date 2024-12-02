using UnityEngine;

public class Arrow : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private int damage;
    [SerializeField] private float xVelocity;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private string targetTag = "Player";
    [SerializeField] private string groundLayerName = "Ground";

    [SerializeField] private bool canMove;
    [SerializeField] private bool flipped;
    private int facingDirection = 1;


    private void Update()
    {
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);

        if (facingDirection == 1 && rb.velocity.x < 0)
        {
            facingDirection = -1;
            sr.flipX = true;
        }
    }

    public void SetupArrow(float _speed)
    {
        sr = GetComponent<SpriteRenderer>();

        xVelocity = _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
            Destroy(gameObject);

        if (collision.CompareTag(targetTag))
        {
            collision.GetComponent<Core>()?.TakeDamage(damage);
        }
    }

    public void FlippedArrow()
    {
        if (flipped)
            return;

        xVelocity = xVelocity * -1;
        flipped = true;
        transform.Rotate(0, 180, 0);
        targetTag = "Enemy";
    }
}
