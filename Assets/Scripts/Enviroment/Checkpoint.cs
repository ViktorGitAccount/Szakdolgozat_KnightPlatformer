using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;
    public string checkpointId;
    public string sceneName;
    public bool activationStatus;
    private bool isPlayerInRange = false;
    [SerializeField] private GameObject interactSymbol;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (string.IsNullOrEmpty(sceneName))
        {
            sceneName = SceneManager.GetActiveScene().name;
        }

        if (interactSymbol != null)
            interactSymbol.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ResetGame();
        }
    }


    [ContextMenu("Generate checkpoint id")]
    private void GenerateId()
    {
        checkpointId = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isPlayerInRange = true;

            ActiveCheckpoint();

            if (interactSymbol != null)
                interactSymbol.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInRange = false;

        if (interactSymbol != null)
            interactSymbol.SetActive(false);
    }

    public void ActiveCheckpoint()
    {
        activationStatus = true;
        animator.SetBool("active", true);
    }

    public void ResetGame()
    {
        GameManager.instance.RestartScene();
    }
}
