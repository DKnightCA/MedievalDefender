using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public float levelTime;
    private float countdownTime;
    public TextMeshProUGUI countdownText;
    public bool isRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        countdownText = GameObject.Find("Countdown").GetComponent<TextMeshProUGUI>();
        countdownTime = levelTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning) {
            UpdateCountdownTime();
        }
    }

    private void UpdateCountdownTime()
    {
        countdownTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(countdownTime / 60F);
        int seconds = Mathf.FloorToInt(countdownTime % 60F);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (countdownTime <= 0)
        {
            countdownText.text = "00:00";
            EventManager.CountdownEnd();

        }
    }

    public void StartCountdown()
    {
        isRunning = true;
    }

    public void StopCountdown()
    {
        isRunning = false;
    }
}
