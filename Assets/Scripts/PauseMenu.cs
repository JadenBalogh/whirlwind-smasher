using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    private bool isPaused;
    public bool IsPaused { get { return isPaused; } set { isPaused = value; } }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (isPaused) Time.timeScale = 1;
        else Time.timeScale = 0;
        UI.SetActive(!UI.activeSelf);
        isPaused = !isPaused;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        UI.SetActive(false);
        isPaused = false;
    }

    public void Restart()
    {

    }

    public void MainMenu()
    {

    }
}
