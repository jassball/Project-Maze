using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour
{
    public GameObject monster;
    public float speed;
    public Vector3 startPosition;
    public Vector3 endPosition;

    private bool hasTriggered = false;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        monster.transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && Vector3.Distance(monster.transform.position, endPosition) > 0.1f)
        {
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, endPosition, speed * Time.deltaTime);
        }
        else if (isMoving)
        {
            isMoving = false;
            monster.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            isMoving = true;
            hasTriggered = true;
        }
    }
}