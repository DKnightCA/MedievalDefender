using System.Collections;
using UnityEngine;

public class CameraRoomMovement : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Camera mainCamera; // Reference to the main camera

    public float screenEdgeThreshold = 0.2f; // Threshold in percentage of screen width/height to consider as edge
    public float moveSpeed; // Speed at which the camera moves to the next position
    public float moveCooldown; // Cooldown time in seconds

    private Vector3 targetPosition;
    private Vector3 initialCameraPosition;
    public float cameraWidth;
    public float cameraHeight;

    private bool canMove = true; // To handle the cooldown

    void Start()
    {
        EventManager.OnCameraGoDown += MoveDown;
        EventManager.OnCameraGoUp += MoveUp;
        EventManager.OnCameraGoLeft += MoveLeft;
        EventManager.OnCameraGoRight += MoveRight;
        initialCameraPosition = mainCamera.transform.position;
        cameraHeight = 2f * mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
        targetPosition = initialCameraPosition;
    }



    private IEnumerator CooldownCoroutine()
    {
        canMove = false;
        yield return new WaitForSeconds(moveCooldown);
        canMove = true;
    }

    private IEnumerator MoveCamera(Vector3 newPosition)
    {
        while (Vector3.Distance(mainCamera.transform.position, newPosition) > 0.2f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        mainCamera.transform.position = newPosition;
    }

    public void MoveLeft()
    {
        if (!canMove)
        {
            return;
        }
        Vector3 playerViewportPos = mainCamera.WorldToViewportPoint(player.position);
        if (playerViewportPos.x < screenEdgeThreshold)
        {
            {
                targetPosition -= new Vector3(cameraWidth, 0, 0);
                StartCoroutine(MoveCamera(targetPosition));
                StartCoroutine(CooldownCoroutine());
            }
        }
    }
    public void MoveRight()
    {
        if (!canMove)
        {
            return;
        }
        Vector3 playerViewportPos = mainCamera.WorldToViewportPoint(player.position);
        if (playerViewportPos.x > 1 - screenEdgeThreshold) {
            targetPosition += new Vector3(cameraWidth, 0, 0);
            StartCoroutine(MoveCamera(targetPosition));
            StartCoroutine(CooldownCoroutine());
        }
    }

    public void MoveUp()
    {
        if (!canMove)
        {
            return;
        }
        Vector3 playerViewportPos = mainCamera.WorldToViewportPoint(player.position);
        if (playerViewportPos.y > 1 - screenEdgeThreshold) {
            targetPosition += new Vector3(0, cameraHeight, 0);
            StartCoroutine(MoveCamera(targetPosition));
            StartCoroutine(CooldownCoroutine());
        }
    }

    public void MoveDown()
    {
        if (!canMove) { return; }
        Vector3 playerViewportPos = mainCamera.WorldToViewportPoint(player.position);
        if (playerViewportPos.y < screenEdgeThreshold) {
            targetPosition -= new Vector3(0, cameraHeight, 0);
            StartCoroutine(MoveCamera(targetPosition));
            StartCoroutine(CooldownCoroutine());
        }
    }
}
