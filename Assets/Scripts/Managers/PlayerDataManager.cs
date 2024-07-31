using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour {

    public static PlayerDataManager Instance { get; private set; }  

    public int experiencePoints;
    public int[] experienceBreakPoints;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevents destruction on scene load
        } else {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    public void AddExperience(int points)
    {
        experiencePoints += points;
        Debug.Log(experiencePoints);
    }
}
