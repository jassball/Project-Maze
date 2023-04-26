using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchMovement : MonoBehaviour
{
    public Transform playerTransform;
    public PlayerMovement playerMovement;

    public float walkBobbingSpeed = 0.18f;
    public float sprintBobbingSpeed = 0.35f;
    public float verticalBobbingAmount = 0.1f;
    public float horizontalBobbingAmount = 0.05f;

    private Vector3 initialPosition;
    private float timer = 0;

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned to TorchMovement script.");
            return;
        }

        initialPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (playerMovement.state == PlayerMovement.MovementState.walking || playerMovement.state == PlayerMovement.MovementState.sprinting)
        {
            float bobbingSpeed = (playerMovement.state == PlayerMovement.MovementState.walking) ? walkBobbingSpeed : sprintBobbingSpeed;
            BobbingEffect(bobbingSpeed);
        }
        else
        {
            timer = 0;
            transform.localPosition = initialPosition;
        }
    }

    private void BobbingEffect(float speed)
    {
        float waveSliceY = 0.0f;
        float waveSliceX = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveSliceY = Mathf.Sin(timer);
            waveSliceX = Mathf.Cos(timer);
            timer = timer + speed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }

        if (waveSliceY != 0 || waveSliceX != 0)
        {
            float translateY = waveSliceY * verticalBobbingAmount;
            float translateX = waveSliceX * horizontalBobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateY = totalAxes * translateY;
            translateX = totalAxes * translateX;
            transform.localPosition = new Vector3(initialPosition.x + translateX, initialPosition.y + translateY, initialPosition.z);
        }
        else
        {
            transform.localPosition = initialPosition;
        }
    }
}