using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private Canvas loadingScreen;
    [SerializeField] private Slider loadingSlider;
    public void StartGame()
    {
        mainMenu.gameObject.SetActive(false);
        loadingScreen.gameObject.SetActive(true);
        StartCoroutine(LoadLevelASync());
    }
    IEnumerator LoadLevelASync()
    {
        AsyncOperation loadOpration = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while(!loadOpration.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOpration.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            QuitGame();
    }
}
