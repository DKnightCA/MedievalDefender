using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // The main camera will move alongside the player. The player has a moveSpeed which may be altered, and a Rigidbody2D.
    // The player does is not trigger.
    public Camera sceneCamera;
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 moveDirection;
    private Vector2 mousePosition;
    
    // The following are lists which track the current active move speed (de)buffs on the player. They are temporary 
    private const float MAX_MOVE_SPEED = 10f;
    private const float MIN_MOVE_SPEED = 1f;


    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        rb.WakeUp();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // el Raw te da 0 o 1
        float moveY = Input.GetAxisRaw("Vertical");


        // Se usa el método .normalized para que no se mueva más rápido en diagonal.
        moveDirection = new Vector2(moveX, moveY).normalized;
        //mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Movement()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        Vector2 dirApuntado = mousePosition - rb.position;
        //float anguloApuntado = Mathf.Atan2(dirApuntado.y, dirApuntado.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = anguloApuntado; // This makes the player rotate towards the mouse
    }

    public Vector2 GetMovementDirection()
    {
        return new Vector2(rb.velocity.x, rb.velocity.y);
    }

    public float GetMoveSpeed()
    {
        return this.moveSpeed;
    }

    // This method changes the move speed of the player permanently. It is not meant to be used for temporary buffs.
    public void SetMoveSpeed(float newMoveSpeed)
    {
        this.moveSpeed = (newMoveSpeed >= MAX_MOVE_SPEED) ? MAX_MOVE_SPEED : newMoveSpeed;
    }

    
    public float AddMoveSpeed(float speedAddition)
    {
        float addedSpeed;
        if(this.moveSpeed + speedAddition >= MAX_MOVE_SPEED)
        {
            addedSpeed = MAX_MOVE_SPEED - this.moveSpeed;
            this.moveSpeed = MAX_MOVE_SPEED;
        } else if (this.moveSpeed + speedAddition <= MIN_MOVE_SPEED)
        {
            addedSpeed = MIN_MOVE_SPEED - this.moveSpeed;
            this.moveSpeed = MIN_MOVE_SPEED;
        } else
        {
            this.moveSpeed += speedAddition;
            addedSpeed = speedAddition;
        }
        return addedSpeed;
    }

    public void AddMoveSpeedModifier(float speedModifier)
    {
        this.moveSpeed = (this.moveSpeed + this.moveSpeed*(speedModifier/100) >= MAX_MOVE_SPEED) ? MAX_MOVE_SPEED : this.moveSpeed+this.moveSpeed*(speedModifier/100);
    }

    public IEnumerator ApplySpeedBoost(float boostAmount, float boostTime)
    {
        float substractSpeed = AddMoveSpeed(boostAmount);
        yield return new WaitForSeconds(boostTime);
        AddMoveSpeed(-substractSpeed);
    }

}
