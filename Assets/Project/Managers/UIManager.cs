using System;
using UnityEngine;

public class UIManager : Manager<UIManager>
{
    [SerializeField] private GameObject pauseMenu;

    private bool isPaused = false;

    private void Start()
    {
        isPaused = false;

        Resume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        if (LevelsManager.Instance.isRestarting) return; // Prevent pausing during a restart

        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void RestartLevel()
    {
        Resume();
        LevelsManager.Instance.RestartLevel();
    }
}