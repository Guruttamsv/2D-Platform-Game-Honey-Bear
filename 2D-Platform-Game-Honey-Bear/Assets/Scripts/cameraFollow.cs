using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform Target;
    public float smoothing;
    Vector3 offset;
    public float lowBoundary; // Set your custom low boundary

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetCamPos = Target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        // Ensure the camera doesn't go below the custom low boundary
        if (transform.position.y < lowBoundary)
        {
            transform.position = new Vector3(transform.position.x, lowBoundary, transform.position.z);
        }
    }
}