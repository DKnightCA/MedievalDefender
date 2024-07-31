using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public float levelTime;
    private float countdownTime;
    public TextMeshProUGUI countdownText;
    // Start is called before the first frame update
    void Start()
    {
        countdownTime = levelTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCountdownTime()
    {
        countdownTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60F);
        int seconds = Mathf.FloorToInt(remainingTime % 60F);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(countdownTime <= 0)
        {
            countdownText = "00:00";
        }
    }
}
