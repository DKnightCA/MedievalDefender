using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int points;
    [SerializeField] TextMeshProUGUI displayPoints;
    [SerializeField] TextMeshProUGUI displayMaxPoints;
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        UpdatePoints();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePoints();
    }

    private void OnEnable()
    {
        EventManager.OnEnemyDefeated += HandleEnemyDefeated;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyDefeated -= HandleEnemyDefeated;
            
    }

    void HandleEnemyDefeated(GameObject enemy)
    {
        points++;
        
        if(PlayerPrefs.GetInt("MejorPuntuacion") < points)
        {
            PlayerPrefs.SetInt("MejorPuntuacion", points);
            PlayerPrefs.GetInt("MejorPuntuacion");
        }
        UpdatePoints();
    }

    void HandlePlayerDefeated()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void CheckHighScore()
    {
        if(points > PlayerPrefs.GetInt("MejorPuntuacion", 0)) // 0 es el valor por defecto (La primera vez que se juega)
        {
            PlayerPrefs.SetInt("MejorPuntuacion", points);
        }
    }

    void UpdatePoints()
    {
        displayPoints.text = $"Puntuación: {points}";
        displayMaxPoints.text = $"Mejor Puntuación: {PlayerPrefs.GetInt("MejorPuntuacion", 0)}";
    }
}
