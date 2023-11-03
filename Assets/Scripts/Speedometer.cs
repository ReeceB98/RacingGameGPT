using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    public TextMeshProUGUI speedText; // Reference to your Text Mesh Pro text.
    public Rigidbody carRigidbody; // Reference to your car's Rigidbody component.

    private void Update()
    {
        if (speedText != null && carRigidbody != null)
        {
            float speed = carRigidbody.velocity.magnitude * 2.23694f; // Convert from meters per second to miles per hour (adjust as needed).

            // Display the speed on the Text Mesh Pro text element.
            speedText.text = "Speed: " + Mathf.Round(speed) + " mph";
        }
    }
}

