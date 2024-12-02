using UnityEngine;

public class ArenaTrigger : MonoBehaviour
{
    public GameObject wallLeft;
    public GameObject wallRight;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (wallLeft != null) wallLeft.SetActive(true);
            if (wallRight != null) wallRight.SetActive(true);
        }
    }
}
