using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSZombie.player;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class CompleteMission : MonoBehaviour
{
    [SerializeField] private Canvas loadingScreen;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TMPro.TMP_Text text;
    [SerializeField] private TMPro.TMP_Text levelCompletetext;
    private int count = 0;
    private int total = 20;
    private void Start()
    {
        levelCompletetext.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        EventListner.OnCollectible += OnCollectibleCollect;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.gameObject.GetComponent<PlayerView>())
        {
            Debug.Log("Player detected");
            if (count == total)
            {
                Debug.Log("Completing level");
                CompleteLevel();
            }
        }
    }
    private void CompleteLevel()
    {
        loadingScreen.gameObject.SetActive(true);
        StartCoroutine(LoadLevelASync());
    }
    private void OnCollectibleCollect()
    {
        if (count < total)
            count++;
        else
            count = total;
        UpdateCount();
    }
    private void UpdateCount()
    {

        if (text != null)
        {
            text.text = $"{count} / {total}";
        }
        if(count  == total)
        {
            levelCompletetext.gameObject.SetActive(true);
        }
    }
    IEnumerator LoadLevelASync()
    {
        AsyncOperation loadOpration = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while (!loadOpration.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOpration.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
    private void OnDisable()
    {
        EventListner.OnCollectible -= OnCollectibleCollect;
    }

}
