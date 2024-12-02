using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 currentTarget;
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private void Start()
    {
        currentTarget = pointB.position;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);


        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {

            currentTarget = currentTarget == pointA.position ? pointB.position : pointA.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
