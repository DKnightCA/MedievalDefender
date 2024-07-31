using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : MonoBehaviour
{
    // Reference to the Player GameObject
    private GameObject player;
    
    private PlayerMovement playerComponent;
    private Rigidbody2D playerRigidbody;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;
    private float dashDirection;

   // Vector2 dirApuntado = mousePosition - rb.position;
   // float anguloApuntado = Mathf.Atan2(dirApuntado.y, dirApuntado.x) * Mathf.Rad2Deg - 90f;

    // The time at which the dash ability will be available again
    private float dashCooldownEndTime = 0.0f;

    private void Start()
    {
        player = this.transform.parent.gameObject;
        playerComponent = player.GetComponent<PlayerMovement>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();

    }
    // Called when the spacebar is pressed
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Check if the dash ability is currently on cooldown
            if (Time.time < dashCooldownEndTime)
            {
                Debug.Log("Dash ability is on cooldown");
            }
            else
            {
                
                

                // If the player has a SkillManager component, trigger the dash ability
                if (playerComponent != null)
                {
                    // Get the current movement direction of the player
                    Vector2 movementDirection = playerComponent.GetMovementDirection();

                    // Trigger the dash ability
                    StartCoroutine(Dash(movementDirection));
                    // Set the dash ability cooldown end time
                    dashCooldownEndTime = Time.time + dashCooldown;
                }
            }
        }
    }

    // Coroutine that handles the dash ability
    private IEnumerator Dash(Vector2 movementDirection)
    {
        // Save the original movement speed of the player
        float originalSpeed = playerComponent.GetComponent<PlayerMovement>().GetMoveSpeed();

        // Increase the player's speed for the duration of the dash

        player.GetComponent<PlayerMovement>().SetMoveSpeed(dashSpeed);

        // Move the player in the current direction for the duration of the dash
        float endTime = Time.time + dashDuration;
        while (Time.time < endTime)
        {
            player.transform.position += (Vector3)movementDirection * Time.deltaTime;
            yield return null;
        }

        // Reset the player's speed to its original value
        player.GetComponent<PlayerMovement>().SetMoveSpeed(originalSpeed);
    }

}