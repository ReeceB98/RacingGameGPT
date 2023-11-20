using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public WheelCollider[] wheelColliders;

    public float accelerationTorque = 5000.0f; // Adjust this value to control acceleration
    public float brakeTorque = 5000.0f; // Adjust this value to control braking
    public float driftTorque = 2000.0f; // Adjust this value to control drifting
    public float maxDriftAngle = 45.0f; // Maximum angle for drifting

    private float horizontalInput;
    private float verticalInput;
    private bool isBraking;

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Check for braking condition
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        float movement = verticalInput * speed;
        float rotation = horizontalInput * rotationSpeed;

        ApplyForceToWheels(movement, rotation);

        // Apply drifting if the car is drifting
        if (IsDrifting())
        {
            ApplyDrift();
        }
    }

    private void ApplyForceToWheels(float movement, float rotation)
    {
        foreach (WheelCollider wheel in wheelColliders)
        {
            if (isBraking)
            {
                // Apply brakes if braking condition is true
                wheel.brakeTorque = brakeTorque;
                wheel.motorTorque = 0.0f;
            }
            else
            {
                // If there's movement, release brakes and apply motor torque
                wheel.brakeTorque = 0.0f;
                wheel.motorTorque = movement * accelerationTorque;
            }

            wheel.steerAngle = rotation;
        }
    }

    private bool IsDrifting()
    {
        // Check for conditions to start drifting
        return isBraking && Mathf.Abs(horizontalInput) > 0.8f;
    }

    private void ApplyDrift()
    {
        foreach (WheelCollider wheel in wheelColliders)
        {
            // Apply a torque to simulate drifting
            wheel.motorTorque = 0.0f;
            wheel.brakeTorque = driftTorque;

            // Rotate the car's Rigidbody to create a visual effect
            float rotationAmount = maxDriftAngle * horizontalInput;
            Quaternion rotation = Quaternion.Euler(0.0f, rotationAmount, 0.0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
        }
    }
}
