using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject pausedWindow;
    [SerializeField]
    GameObject optionsWindow;
    [SerializeField]
    GameObject helpWindow;
    [SerializeField]
    GameObject menuUI;

    AudioManager audioManager;

    enum MenuStates { Playing, Pause, Options, Help, Dead, MainMenu}
    MenuStates currentState;

    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            currentState = MenuStates.MainMenu;
        else
            currentState = MenuStates.Playing;
    }

    void Start()
    {
        audioManager = AudioManager.instance;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape") && currentState == MenuStates.Pause)
        {
            currentState = MenuStates.Playing;
        }
        else if (Input.GetKeyDown("escape") && currentState == MenuStates.Playing)
        {
            currentState = MenuStates.Pause;
        }

        switch (currentState)
        {
            case MenuStates.MainMenu:
                currentState = MenuStates.MainMenu;
                pausedWindow.SetActive(true);
                optionsWindow.SetActive(false);
                helpWindow.SetActive(false);
                menuUI.SetActive(true);                            
                break;
            case MenuStates.Playing:
                currentState = MenuStates.Playing;
                pausedWindow.SetActive(false);
                optionsWindow.SetActive(false);
                helpWindow.SetActive(false);
                menuUI.SetActive(false);
                Time.timeScale = 1;
                break;

            case MenuStates.Pause:
                currentState = MenuStates.Pause;
                pausedWindow.SetActive(true);
                optionsWindow.SetActive(false);
                helpWindow.SetActive(false);
                menuUI.SetActive(true);
                Time.timeScale = 0;
                break;

            case MenuStates.Options:
                currentState = MenuStates.Options;
                pausedWindow.SetActive(false);
                optionsWindow.SetActive(true);
                helpWindow.SetActive(false);
                menuUI.SetActive(true);
                Time.timeScale = 0;
                break;

            case MenuStates.Help:
                currentState = MenuStates.Help;
                pausedWindow.SetActive(false);
                optionsWindow.SetActive(false);
                helpWindow.SetActive(true);
                menuUI.SetActive(true);
                Time.timeScale = 0;
                break;

        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(GameManager.gm.GetCurrentLevel());
    }

    public void DisplayOptions()
    {
        currentState = MenuStates.Options;
    }

    public void DisplayHelp()
    {
        currentState = MenuStates.Help;
    }

    public void Resume()
    {
        currentState = MenuStates.Playing;
    }

    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void BackButton()
    {
        currentState = MenuStates.Pause;
    }

    public void SetSFXVolume(float level)
    {
        audioManager.SetSFXVolume(level);
    }

    public void SetMusicVolume(float level)
    {
        audioManager.SetMusicVolume(level);
    }

    public void SetMasterVolume(float level)
    {
        audioManager.SetMasterVolume(level);
    }
}
