using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<GameObject> OnEnemyDefeated;
    public static event Action OnLevelPassed;
    public static event Action OnTogglePauseMenu;

    public static event Action OnCameraGoLeft;
    public static event Action OnCameraGoRight;
    public static event Action OnCameraGoUp;
    public static event Action OnCameraGoDown;

    public static event Action<MazeRoom> OnEnterRoom;
    public static event Action<MazeRoom> OnExitRoom;


    public static void EnemyDefeated(GameObject enemy)
    {
        OnEnemyDefeated?.Invoke(enemy);
    }

    public static void LevelPassed()
    {
        OnLevelPassed?.Invoke();
    }

    public static void TogglePauseMenu()
    {
        OnTogglePauseMenu?.Invoke();
    }

    public static void CameraGoLeft()
    {
        OnCameraGoLeft?.Invoke();
    }

    public static void CameraGoRight()
    {
        OnCameraGoRight?.Invoke();
    }

    public static void CameraGoUp()
    {
        OnCameraGoUp?.Invoke();
    }

    public static void CameraGoDown()
    {
        OnCameraGoDown?.Invoke();
    }

    public static void EnterRoom(MazeRoom room)
    {
        OnEnterRoom?.Invoke(room);
    }

    public static void ExitRoom(MazeRoom room)
    {
        OnExitRoom?.Invoke(room);
    }
}
