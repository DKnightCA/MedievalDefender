using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{

    
    private bool isPaused;

    public Tilemap borderTilemap;
    // Start is called before the first frame update

    void OnEnable()
    {
        EventManager.OnLevelPassed += PassLevel;
        EventManager.OnTogglePauseMenu += TogglePause;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EventManager.TogglePauseMenu();
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
        RemoveTile(new Vector3Int(-1, 4, 0));
        RemoveTile(new Vector3Int(0, 4, 0));
        RemoveTile(new Vector3Int(1, 4, 0));
        // Deactivate MonsterSpawners and monsters. Activate LevelPassed Canvas
    }

    public void RemoveTile(Vector3Int tilePosition)
    {
        // If newTile is null, it will remove the tile at the specified position
        borderTilemap.SetTile(tilePosition, null);
    }
}
