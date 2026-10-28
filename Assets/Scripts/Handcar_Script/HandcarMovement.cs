using UnityEngine;
using UnityEngine.InputSystem;

public class HandcarMovement
{
    [SerializeField, Min(0f)] private float speed = 10f;
    [SerializeField, Min(1f)] private float accilirate = 5f;
    [SerializeField, Min(0f)] private float rotationSpeed = 180f;

    private float currentSpeed = 0f;
    private bool isMoving = false;

    private void SwitchMove()
    {
        if (Keyboard.current == null)
            return;


        if (Keyboard.current.eKey.wasPressedThisFrame)
            isMoving = !isMoving;
    }

    private void Move()
    {
        if (!isMoving && currentSpeed == 0f)
            return;

        if (!isMoving && currentSpeed > 0f)
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, accilirate * Time.deltaTime);
        else 
            currentSpeed = Mathf.MoveTowards(currentSpeed, speed, accilirate * Time.deltaTime);
    }
}