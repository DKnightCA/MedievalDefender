using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraWidthAdjuster : MonoBehaviour
{
    public float desiredWidth = 10f; // Desired width of the camera view

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        AdjustCameraWidth();
    }

    void AdjustCameraWidth()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        float desiredAspect = desiredWidth / (2f * mainCamera.orthographicSize);

        if (currentAspect >= desiredAspect)
        {
            // Current aspect ratio is wider than desired, adjust viewport width
            float scaleWidth = desiredAspect / currentAspect;
            Rect rect = mainCamera.rect;
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
            mainCamera.rect = rect;
        }
        else
        {
            // Current aspect ratio is narrower or equal to desired, full width
            Rect rect = mainCamera.rect;
            rect.width = 1f;
            rect.x = 0f;
            mainCamera.rect = rect;
        }
    }

    void Update()
    {
        AdjustCameraWidth();
    }
}
