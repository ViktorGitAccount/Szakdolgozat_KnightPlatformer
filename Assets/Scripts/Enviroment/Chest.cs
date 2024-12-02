using UnityEngine;

public class Chest : MonoBehaviour, ISaveManager
{
    [SerializeField] private string chestID;
    private Animator animator;
    [SerializeField] private GameObject interactSymbol;
    private bool isPlayerInRange = false;
    private bool isOpen = false;
    private ItemDrop chestDrop;
    public Stat currencyDrop;

    private void Start()
    {
        chestDrop = GetComponent<ItemDrop>();
        animator = GetComponent<Animator>();


        if (interactSymbol != null)
            interactSymbol.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
            OpenChest();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isPlayerInRange = true;

            if (interactSymbol != null)
                interactSymbol.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isPlayerInRange = false;

            if (interactSymbol != null)
                interactSymbol.SetActive(false);
        }
    }

    private void OpenChest()
    {
        if (!isOpen)
        {
            isOpen = true;

            animator.SetBool("open", true);

            chestDrop.GenerateDrop();
            PlayerManager.instance.currency += currencyDrop.GetValue();

            if (interactSymbol != null)
                interactSymbol.SetActive(false);
        }
    }

    public void LoadData(GameData _data)
    {
        if (_data.chestStates != null && _data.chestStates.TryGetValue(chestID, out bool isChestOpen))
        {
            isOpen = isChestOpen;
            animator.SetBool("open", isOpen);

        }
    }

    public void SaveData(ref GameData _data)
    {
        if (_data.chestStates == null)
        {
            _data.chestStates = new Serializeable_Dictionary<string, bool>();
        }

        _data.chestStates[chestID] = isOpen;
    }
}
