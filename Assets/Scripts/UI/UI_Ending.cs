using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Ending : MonoBehaviour
{
    public void GoMainMenu()
    {
        SaveManager.instance.SaveGame();

        SceneManager.LoadScene("MainMenu");
    }
}
