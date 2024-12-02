using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private int damageAmount = 50; 
    [SerializeField] private Vector2 knockbackDirection = new Vector2(0, 0);
    [SerializeField] private float knockbackDuration = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Player player = collision.GetComponent<Player>();
        if (player != null)
        {

            player.TakeDamage(damageAmount);


            player.StartCoroutine(player.HitKnockback(knockbackDirection, knockbackDuration));
        }
    }
}
