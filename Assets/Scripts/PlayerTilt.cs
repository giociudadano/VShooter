using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTilt : MonoBehaviour
{
    [SerializeField] private float maxTiltAngle = 90f;
    [SerializeField] private float tiltSpeed = 100f;
    private float currentTilt = 0f;
    private Quaternion baseRotation;

    void Start()
    {
        baseRotation = transform.rotation;
    }

    void Update()
    {
        //  Check for left mouse button click
        if (Input.GetMouseButton(1)) {
            //  Rotate to the left
            currentTilt = Mathf.Clamp(currentTilt + tiltSpeed * Time.deltaTime, -maxTiltAngle, maxTiltAngle);
        } else if (Input.GetMouseButton(0)) {   //  Right mouse button click
            //  Rotate to the right
            currentTilt = Mathf.Clamp(currentTilt - tiltSpeed * Time.deltaTime, -maxTiltAngle, maxTiltAngle);
        }

        //  Smoothly interpolate towards the target rotation
        Quaternion targetRotation = Quaternion.Euler(baseRotation.x, currentTilt, baseRotation.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
    }
}
