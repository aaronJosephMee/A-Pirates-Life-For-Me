using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;   // Movement speed
    public float rotationSpeed = 180f;  // Rotation speed

    void Update()
    {
        // Get input for movement
        float verticalInput = Input.GetAxis("Vertical");

        // Move forward or backward based on input
        Vector3 movement = transform.forward * verticalInput * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Get input for rotation
        float horizontalInput = Input.GetAxis("Horizontal");

        // Rotate the player based on input
        Vector3 rotation = new Vector3(0f, horizontalInput * rotationSpeed * Time.deltaTime, 0f);
        transform.Rotate(rotation);
    }
}
