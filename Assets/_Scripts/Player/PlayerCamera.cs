using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Mouse sensitivity
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    public PlayerHealth playerHealth;

    private bool canProcessInput = false;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize rotation values
        xRotation = WrapAngle(transform.eulerAngles.x);
        yRotation = orientation.eulerAngles.y;

        StartCoroutine(EnableInputProcessing());
    }

    IEnumerator EnableInputProcessing()
    {
        yield return new WaitForSeconds(0.5f);
        canProcessInput = true;
    }

    // Update is called once per frame
    void Update() {
        if (playerHealth.currentHealth > 0 && canProcessInput)
        {
            // Get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.fixedDeltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.fixedDeltaTime;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Rotate camera and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    float WrapAngle(float angle) {
        angle %= 360;
        if (angle > 180)
            return angle - 360;
        return angle;
    }
}
