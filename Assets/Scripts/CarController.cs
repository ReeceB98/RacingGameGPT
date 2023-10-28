using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public WheelCollider[] wheelColliders; // Reference to the Wheel Colliders

    public float accelerationTorque = 5000.0f; // Adjust this value to control acceleration
    public float brakeTorque = 5000.0f; // Adjust this value to control braking

    private float horizontalInput;
    private float verticalInput;

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        float movement = verticalInput * speed;
        float rotation = horizontalInput * rotationSpeed;

        // Apply forces to move and rotate the car
        ApplyForceToWheels(movement, rotation);

        // Apply brakes or reverse when the player wants to stop or go backward
        if (movement > 0)
        {
            ApplyAcceleration(movement);
        }
        else if (movement < 0)
        {
            ApplyReverse(movement);
        }
        else
        {
            ApplyBrakes();
        }
    }

    private void ApplyForceToWheels(float movement, float rotation)
    {
        foreach (WheelCollider wheel in wheelColliders)
        {
            wheel.motorTorque = movement * accelerationTorque;
            wheel.steerAngle = rotation;
        }
    }

    private void ApplyAcceleration(float movement)
    {
        foreach (WheelCollider wheel in wheelColliders)
        {
            wheel.motorTorque = movement * accelerationTorque;
            wheel.brakeTorque = 0.0f;
        }
    }

    private void ApplyReverse(float movement)
    {
        foreach (WheelCollider wheel in wheelColliders)
        {
            // Apply negative motor torque to make the car move backward
            wheel.motorTorque = movement * accelerationTorque;
            wheel.brakeTorque = 0.0f;
        }
    }

    private void ApplyBrakes()
    {
        foreach (WheelCollider wheel in wheelColliders)
        {
            wheel.motorTorque = 0.0f;
            wheel.brakeTorque = brakeTorque;
        }
    }
}
