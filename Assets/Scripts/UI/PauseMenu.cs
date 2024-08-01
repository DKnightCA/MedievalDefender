using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool activeState = false;

    void OnEnable()
    {
        EventManager.OnTogglePauseMenu += ToggleUI;
    }

    void OnDisable()
    {
        EventManager.OnTogglePauseMenu -= ToggleUI;
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
