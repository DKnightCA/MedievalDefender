using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{

    public float levelTime;
    private float countdownTime;
    public TextMeshProUGUI countdownText;
    private bool isPaused;
    // Start is called before the first frame update

    void OnEnable()
    {
        EventManager.OnLevelPassed += PassLevel;
    }

    void OnDisable()
    {
        EventManager.OnLevelPassed -= PassLevel;
    }

    void Start()
    {
        countdownTime = levelTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
        UpdateCountdownTime();
    }

    private void UpdateCountdownTime()
    {
        countdownTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(countdownTime / 60F);
        int seconds = Mathf.FloorToInt(countdownTime % 60F);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(countdownTime <= 0)
        {
            countdownText.text = "00:00";
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1; // Freezes or resumes time

        // Additional logic for pausing other activities (animations, audio, etc.)
    }

    private void PassLevel()
    {
        // Deactivate MonsterSpawners and monsters. Activate LevelPassed Canvas
    }
}
