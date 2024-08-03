using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScaler : MonoBehaviour
{
    public float baseOrthographicSize = 5f; // Base size of the camera orthographic view
    public float baseAspectRatio = 16f / 9f; // Base aspect ratio (e.g., 16:9)

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        AdjustCameraSize();
    }

    void AdjustCameraSize()
    {
        float currentAspectRatio = (float)Screen.width / Screen.height;
        if (currentAspectRatio >= baseAspectRatio)
        {
            mainCamera.orthographicSize = baseOrthographicSize;
        }
        else
        {
            float differenceInSize = baseAspectRatio / currentAspectRatio;
            mainCamera.orthographicSize = baseOrthographicSize * differenceInSize;
        }
    }

    void Update()
    {
        AdjustCameraSize();
    }
}
