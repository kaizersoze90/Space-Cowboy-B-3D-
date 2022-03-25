using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xMin = 5f;
    [SerializeField] float xMax = 5f;
    [SerializeField] float yMin = 5f;
    [SerializeField] float yMax = 5f;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = -2f;
    [SerializeField] float controlRollFactor = -10f;
    [SerializeField] float smoothInputSpeed = 0.2f;

    Vector2 moveInput;
    Vector2 smoothedMoveInput;
    Vector2 smoothedInputVelocity;

    void Start()
    {

    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        SmoothInputs();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
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
}
