using UnityEngine;
using TMPro;

public class SwitchController : MonoBehaviour
{
    public LayerMask WhatIsPlayer;
    public GameObject lever;
    public GameObject hatch;
    public TextMeshProUGUI promptText;
    public float leverRotationX = 40f;
    public float rotationDuration = 1f;
    private bool isPlayerInRange;
    private bool hasSwitched;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private Quaternion hatchInitialRotation;
    private Quaternion hatchTargetRotation;
    private float rotationStartTime;

    private void Start()
    {
        promptText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & WhatIsPlayer) != 0 && !hasSwitched)
        {
            isPlayerInRange = true;
            promptText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & WhatIsPlayer) != 0)
        {
            isPlayerInRange = false;
            promptText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !hasSwitched)
        {
            initialRotation = lever.transform.rotation;
            targetRotation = initialRotation * Quaternion.Euler(leverRotationX, 0, 0);

            hatchInitialRotation = hatch.transform.rotation;
            hatchTargetRotation = hatchInitialRotation * Quaternion.Euler(90, 0, 0);

            rotationStartTime = Time.time;
            hasSwitched = true;
        }

        if (hasSwitched && Time.time <= rotationStartTime + rotationDuration)
        {
            float t = (Time.time - rotationStartTime) / rotationDuration;
            lever.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);
            hatch.transform.rotation = Quaternion.Lerp(hatchInitialRotation, hatchTargetRotation, t);
        }
        else if (hasSwitched && promptText.gameObject.activeSelf)
        {
            promptText.gameObject.SetActive(false);
        }
    }
}