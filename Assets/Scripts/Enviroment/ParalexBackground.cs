using UnityEngine;

public class ParalexBackground : MonoBehaviour
{

    private GameObject cam;

    [SerializeField] private float parallexEffect;


    private float xPosition;
    private float yPosition;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");

        xPosition = transform.position.x;
        yPosition = transform.position.y;
    }

    private void Update()
    {
        float xdistanceToMove = cam.transform.position.x * parallexEffect;
        float ydistanceToMove = cam.transform.position.y * parallexEffect;

        transform.position = new Vector3(xPosition + xdistanceToMove, yPosition + ydistanceToMove);
    }
}
