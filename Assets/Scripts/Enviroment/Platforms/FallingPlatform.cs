using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;


    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float destroyDelay = 2f;


    private bool isFalling = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling)
            return;

        if (collision.gameObject.GetComponent<Player>() != null)
            StartCoroutine(FallAfterDelay());

    }

    private IEnumerator FallAfterDelay()
    {
        isFalling = true;

        yield return new WaitForSeconds(fallDelay);

        rb.bodyType = RigidbodyType2D.Dynamic;

        Destroy(gameObject, destroyDelay);
    }

}
