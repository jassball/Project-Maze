using UnityEngine;

public class EntryMazeDoor : MonoBehaviour
{
    private bool hasOpened = false;
    public float rotationAngle = 90f;
    public float rotationSpeed = 2f;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, rotationAngle, 0));
    }

    void Update()
    {
        if (hasOpened)
        {
            OpenDoor();
        }
    }

    public void Open()
    {
        if (!hasOpened)
        {
            hasOpened = true;
        }
    }

    private void OpenDoor()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * rotationSpeed);
    }
}
