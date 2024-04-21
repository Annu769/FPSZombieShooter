using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FPSZombie.Event;


public class GameUIManager : MonoBehaviour
{
    private bool gameIsPaused;
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject playerHealthBarObject;
  
   
    private void Awake()
    {
        canvasAnimator = GetComponent<Animator>();

       
    }
    private void Start()
    {
        EventService.Instance.OnSetMaxHealthBar += SetMaxHealth;
        EventService.Instance.OnSetPlayerHealthBar += SetPlayerHealth;
        EventService.Instance.OnGameOver += StartGameOver;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void SetMaxHealth(int _maxHealth)
    {
        playerHealthBar.maxValue = _maxHealth;
    }
    public void SetPlayerHealth(int health)
    {
        playerHealthBar.value = health;
    }
    public void StartGameOver()
    {
        canvasAnimator.SetTrigger("GameOver");
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        Cursor.visible = true;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        playerHealthBarObject.SetActive(false);
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }
    public void ReStartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    

    private void OnDisable()
    {
        EventService.Instance.OnSetMaxHealthBar -= SetMaxHealth;
        EventService.Instance.OnSetPlayerHealthBar -= SetPlayerHealth;
        EventService.Instance.OnGameOver -= StartGameOver;
    }

}
