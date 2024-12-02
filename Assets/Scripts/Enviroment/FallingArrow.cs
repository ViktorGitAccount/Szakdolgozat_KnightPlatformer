using UnityEngine;

public class FallingArrow : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float resetTime = 3f;
    public int damage = 50;

    private Vector2 originalPosition;
    private bool canFall = false;
    private int groundLayer;

    private void Start()
    {
        originalPosition = transform.position;
        groundLayer = LayerMask.NameToLayer("Ground");
        Invoke("ResetSpike", resetTime);
    }

    private void Update()
    {
        StartTheSpikeFall();
    }

    public void StartTheSpikeFall()
    {
        if (canFall)
        {
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
            ResetSpike();
        }
        else if (collision.gameObject.layer == groundLayer)
            ResetSpike();

    }

    private void ResetSpike()
    {
        transform.position = originalPosition;

        CancelInvoke("StartFalling");
        CancelInvoke("ResetSpike");

        Invoke("StartFalling", 3f);
    }


    public void StartFalling()
    {
        Invoke("ResetSpike", resetTime);
    }

    public void EnableFalling()
    {
        canFall = true;
        Invoke("ResetSpike", resetTime);
    }
}
