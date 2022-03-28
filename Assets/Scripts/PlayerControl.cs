using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("Laser Array")]
    [SerializeField] GameObject[] lasers;

    [Header("Control Settings")]
    [Tooltip("Adjusts ship up-down-right-left direction speed")]
    [SerializeField] float controlSpeed = 10f;
    [Tooltip("Adjusts min X axis boundry to prevent the ship flyaway from screen")]
    [SerializeField] float xMin = 5f;
    [Tooltip("Adjusts max X axis boundry to prevent the ship flyaway from screen")]
    [SerializeField] float xMax = 5f;
    [Tooltip("Adjusts min Y axis boundry to prevent the ship flyaway from screen")]
    [SerializeField] float yMin = 5f;
    [Tooltip("Adjusts max Y axis boundry to prevent the ship flyaway from screen")]
    [SerializeField] float yMax = 5f;

    [Header("Screen Position Tuning Settings")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = -2f;

    [Header("Player Input Tuning Settings")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -10f;
    [SerializeField] float smoothInputSpeed = 0.2f;

    Vector2 moveInput;
    Vector2 smoothedMoveInput;
    Vector2 smoothedInputVelocity;

    public bool isAlive = true;
    bool isFiring;

    void Start()
    {

    }

    void Update()
    {
        if (!isAlive) { return; }
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
        SmoothInputs();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        isFiring = value.isPressed;
    }

    void SmoothInputs()
    {
        smoothedMoveInput = Vector2.SmoothDamp
        (smoothedMoveInput, moveInput, ref smoothedInputVelocity, smoothInputSpeed);
    }

    void ProcessTranslation()
    {
        float xOffset = smoothedMoveInput.x * controlSpeed * Time.deltaTime;
        float yOffset = smoothedMoveInput.y * controlSpeed * Time.deltaTime;

        float xClampedPos = Mathf.Clamp(transform.localPosition.x + xOffset, xMin, xMax);
        float yClampedPos = Mathf.Clamp(transform.localPosition.y + yOffset, yMin, yMax);

        transform.localPosition = new Vector3(xClampedPos, yClampedPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = smoothedMoveInput.y * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = smoothedMoveInput.x * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (isFiring)
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool state)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = state;
        }
    }


}
