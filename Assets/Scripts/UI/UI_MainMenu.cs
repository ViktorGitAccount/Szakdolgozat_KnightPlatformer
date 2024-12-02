using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    private GameData data;
    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;


    private void Start()
    {

        if(SaveManager.instance.HasSavedData() == false)
            continueButton.SetActive(false);
    }


    public void ContinueGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect(1f));
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSaveData();
        StartCoroutine(LoadSceneWithFadeEffect(1f));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        if (SaveManager.instance.HasSavedData() == true)
        {
            data = SaveManager.instance.gameData;
            sceneName = data.sceneOfCheckpoint;
        }

        SceneManager.LoadScene(sceneName);
    }

}
