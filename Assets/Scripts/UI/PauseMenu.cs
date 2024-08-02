using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool activeState = false;

    void Start()
    {
        EventManager.OnTogglePauseMenu += ToggleUI;
        this.gameObject.SetActive(activeState);
    }

    public void PlayGame()
    {
        EventManager.TogglePauseMenu();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToggleUI()
    {
        activeState = !activeState;
        this.gameObject.SetActive(activeState);
    }
}
