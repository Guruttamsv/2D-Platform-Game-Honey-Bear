using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0f, 0f, 0f);
    public Vector3 endPosition = new Vector3(5f, 0f, 0f);
    public float speed = 2.0f;

    private bool movingToEnd = true;

    void Update()
    {
        // Calculate the direction based on the current movement
        Vector3 direction = movingToEnd ? endPosition - transform.position : startPosition - transform.position;

        // Normalize the direction to ensure consistent speed
        direction.Normalize();

        // Move the platform
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the platform has reached the target position
        if ((movingToEnd && Vector3.Distance(transform.position, endPosition) < 0.1f) ||
            (!movingToEnd && Vector3.Distance(transform.position, startPosition) < 0.1f))
        {
            // Toggle the direction
            movingToEnd = !movingToEnd;
        }
    }
}
