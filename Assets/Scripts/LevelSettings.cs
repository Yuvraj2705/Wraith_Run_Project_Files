using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSettings : MonoBehaviour
{
    [Header("Text Status")]
    [SerializeField] TextMeshProUGUI TimeStatus;
    [SerializeField] TextMeshProUGUI CoinStatus;
    [SerializeField] TextMeshProUGUI LifeStatus;

    [Header("Menus")]
    [SerializeField] GameObject PauseButton;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject InGameUi;
    [SerializeField] GameObject GameOverMenu;

    [Header("Variables")]
    public int CoinValue = 0;
    public int Life = 3;
    public bool GameOverCheck = true;

    [Header("Misc")]
    [SerializeField] AudioSource audioSource;

    void Awake()
    {
        if(PauseButton != null)
        {
            PauseButton.SetActive(true);
        }
        if(PauseMenu != null)
        {
            PauseMenu.SetActive(false);
        }  
        if(InGameUi != null)
        {
            InGameUi.SetActive(true);
        }
        if(GameOverMenu != null)
        {
            GameOverMenu.SetActive(false);
        }  
        if(LifeStatus != null)
        {
            LifeStatus.text = Life.ToString();
        }
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        if(GameOverCheck)
        {
            if(TimeStatus != null)
            {
                TimeStatus.text = ((int)Time.timeSinceLevelLoad).ToString();
            }

            if(CoinStatus != null)
            {
                CoinStatus.text = CoinValue.ToString();
            }
        
            if(LifeStatus != null)
            {
                HealthCheck();
            }
        }
    } 

    void HealthCheck()
    {
        LifeStatus.text = Life.ToString();
        if(Life == 0)
        {
            Time.timeScale = 0f;
            GameOverCheck = false;
            GameOverMenuEnable();
        }
    }

    void GameOverMenuEnable()
    {
        GameOverMenu.SetActive(true);
        PauseButton.SetActive(false);
        PauseMenu.SetActive(false);
        InGameUi.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PauseButton.SetActive(false);
        PauseMenu.SetActive(true);
        InGameUi.SetActive(false);
        audioSource.Pause();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PauseButton.SetActive(true);
        PauseMenu.SetActive(false);
        InGameUi.SetActive(true);
        audioSource.Play();
    }

    public void ResetLevel()
    {
        Time.timeScale = 1f;
        PauseButton.SetActive(true);
        PauseMenu.SetActive(false);
        InGameUi.SetActive(true);
        SceneManager.LoadScene("Game");
    }

    public void PlayLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
