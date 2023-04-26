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

    public GameObject door1;
    public GameObject door2;
    public float doorMovementDistance = 3f;
    private Vector3 door1InitialPosition;
    private Vector3 door1TargetPosition;
    private Vector3 door2InitialPosition;
    private Vector3 door2TargetPosition;

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

            door1InitialPosition = door1.transform.localPosition;
            door1TargetPosition = door1InitialPosition + new Vector3(0, -doorMovementDistance, 0);

            door2InitialPosition = door2.transform.localPosition;
            door2TargetPosition = door2InitialPosition + new Vector3(0, -doorMovementDistance, 0);

            rotationStartTime = Time.time;
            hasSwitched = true;

            Debug.Log("Switch activated");
        }

        if (hasSwitched && Time.time <= rotationStartTime + rotationDuration)
        {
            float t = (Time.time - rotationStartTime) / rotationDuration;
            lever.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);
            hatch.transform.rotation = Quaternion.Lerp(hatchInitialRotation, hatchTargetRotation, t);

            float tt = (Time.time - rotationStartTime) / rotationDuration;
            door1.transform.localPosition = Vector3.Lerp(door1InitialPosition, door1TargetPosition, tt);
            door2.transform.localPosition = Vector3.Lerp(door2InitialPosition, door2TargetPosition, tt);

            Debug.Log("Door 1 Position: " + door1.transform.localPosition);
            Debug.Log("Door 2 Position: " + door2.transform.localPosition);
        }
        else if (hasSwitched && promptText.gameObject.activeSelf)
        {
            promptText.gameObject.SetActive(false);
        }
    }
}