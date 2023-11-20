using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public WheelCollider[] wheelColliders;

    private float horizontalInput;
    private float verticalInput;

    // Drift parameters
    public float driftTorque = 2000f;
    public float maxDriftAngle = 45f;

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        float movement = verticalInput * speed;
        float rotation = horizontalInput * rotationSpeed;

        ApplyForceToWheels(movement, rotation);

        // Check for drifting condition
        if (IsDrifting())
        {
            ApplyDrift();
        }
    }

    private void ApplyForceToWheels(float movement, float rotation)
    {
        foreach (WheelCollider wheel in wheelColliders)
        {
            wheel.motorTorque = movement;
            wheel.steerAngle = rotation;
        }
    }

    private bool IsDrifting()
    {
        // Check for conditions to start drifting
        // For example, you might check if the player is holding a specific input key
        // or if the car is turning sharply.
        return Input.GetKey(KeyCode.Space) && Mathf.Abs(horizontalInput) > 0.8f;
    }

    private void ApplyDrift()
    {
        // Apply a torque to simulate drifting
        foreach (WheelCollider wheel in wheelColliders)
        {
            wheel.motorTorque = 0f;
            wheel.brakeTorque = driftTorque;
        }

        // Rotate the car's Rigidbody to create a visual effect
        float rotationAmount = maxDriftAngle * horizontalInput;
        Quaternion rotation = Quaternion.Euler(0f, rotationAmount, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
    }
}

