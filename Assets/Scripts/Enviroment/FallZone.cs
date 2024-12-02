using UnityEngine;

public class FallZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Core>() != null)
            collision.GetComponent<Core>().KillCharacter();
        else
            Destroy(collision.gameObject);
    }
}
