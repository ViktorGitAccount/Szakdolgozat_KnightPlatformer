using UnityEngine;

public class ItemObject_Trigger : MonoBehaviour
{

    private ItemObject myItemObject => GetComponentInParent<ItemObject>();
    private bool isPlayerInRange = false;
    private Player player;
    [SerializeField] private GameObject interactSymbol;

    private void Start()
    {
        if (interactSymbol != null)
            interactSymbol.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
            myItemObject.PickUpItem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isPlayerInRange = true;
            player = collision.GetComponent<Player>();

            if (interactSymbol != null)
                interactSymbol.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isPlayerInRange = false; ;
            player = null;

            if (interactSymbol != null)
                interactSymbol.SetActive(false);
        }
    }
}
