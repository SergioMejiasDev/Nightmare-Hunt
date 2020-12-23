using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsible for the camera to follow the character.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] [Range (0.0f , 1.0f)] float smoothing = 0.5f;
    [SerializeField] bool interpolation = false;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 cameraPosition = target.position + offset;

        if (interpolation)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothing);
        }
        else
        {
            transform.position = cameraPosition;
        }
    }
}
