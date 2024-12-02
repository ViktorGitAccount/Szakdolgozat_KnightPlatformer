using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour
{
    private bool isPlayerInRange = false;
    [SerializeField] private GameObject interactSymbol;
    [SerializeField] private string sceneName;

    private void Awake()
    {
        if (interactSymbol != null)
            interactSymbol.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            EndingActivate();
        }
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

    private void EndingActivate()
    {
        SaveManager.instance.SaveGame();
        SceneManager.LoadScene(sceneName);
    }
}
