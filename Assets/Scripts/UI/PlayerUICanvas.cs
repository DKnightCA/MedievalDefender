using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUICanvas : MonoBehaviour
{
    private bool activeState = true;
    void Start()
    {
        EventManager.OnTogglePauseMenu += ToggleUI;
        this.gameObject.SetActive(activeState);
    }

    public void ToggleUI()
    {
        activeState = !activeState;
        this.gameObject.SetActive(activeState);
    }
}
