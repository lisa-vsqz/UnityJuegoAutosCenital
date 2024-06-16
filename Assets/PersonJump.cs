using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleJumpController : MonoBehaviour
{
    public float jumpHeight = 1.0f; // Height of the jump
    public float jumpSpeed = 1.0f;  // Speed of the jump
    public float detectionRadius = 5.0f; // Radius within which the car will trigger the jump

    private List<Transform> people = new List<Transform>();
    private Transform playerCar;
    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, bool> isJumping = new Dictionary<Transform, bool>();

    void Start()
    {
        // Get all children of the parent object
        foreach (Transform child in transform)
        {
            people.Add(child);
            originalPositions[child] = child.position;
            isJumping[child] = false;
        }

        playerCar = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerCar == null)
        {
            return;
        }

        foreach (Transform person in people)
        {
            float distance = Vector3.Distance(playerCar.position, person.position);
            if (distance <= detectionRadius && !isJumping[person])
            {
                StartCoroutine(Jump(person));
            }
            else if (distance > detectionRadius && isJumping[person])
            {
                StopCoroutine(Jump(person));
                person.position = originalPositions[person];
                isJumping[person] = false;
            }
        }
    }

    IEnumerator Jump(Transform person)
    {
        isJumping[person] = true;
        Vector3 originalPosition = originalPositions[person];
        while (isJumping[person])
        {
            float newY = Mathf.Sin(Time.time * jumpSpeed) * jumpHeight + originalPosition.y;
            person.position = new Vector3(person.position.x, newY, person.position.z);
            yield return null;
        }
    }
}
