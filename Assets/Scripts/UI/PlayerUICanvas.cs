using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUICanvas : MonoBehaviour
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
        SceneManager.LoadScene("Level");
    }

    public void ToggleUI()
    {
        activeState = !activeState;
        this.gameObject.SetActive(activeState);
    }
}
