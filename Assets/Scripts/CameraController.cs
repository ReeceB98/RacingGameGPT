using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Assign the car GameObject to this field.
    public float distance = 10.0f; // Distance from the car to the camera.
    public float height = 3.0f; // Height of the camera above the car.
    public float smoothSpeed = 5.0f; // Smoothness of camera movement.

    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0, height, -distance);
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.LookAt(target.position);
    }
}