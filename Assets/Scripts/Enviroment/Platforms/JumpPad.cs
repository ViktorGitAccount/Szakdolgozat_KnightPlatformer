using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float bounce = 20f;
    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.otherCollider == circleCollider)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
