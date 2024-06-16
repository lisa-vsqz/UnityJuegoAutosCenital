using System.Collections;
using UnityEngine;

public class PersonJump : MonoBehaviour
{
    public float jumpHeight = 1.5f; // Height of the jump
    public float jumpSpeed = 10f;  // Speed of the jump
    public float detectionRadius = 15f; // Radius within which the car will trigger the jump

    private bool isJumping = false;
    private Vector3 originalPosition;
    private Transform playerCar;

    void Start()
    {
        originalPosition = transform.position;
        playerCar = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerCar == null)
        {
            return;
        }

        float distance = Vector3.Distance(playerCar.position, transform.position);
        if (distance <= detectionRadius && !isJumping)
        {
            StartCoroutine(Jump());
        }
        else if (distance > detectionRadius && isJumping)
        {
            StopCoroutine(Jump());
            transform.position = originalPosition;
            isJumping = false;
        }
    }

    IEnumerator Jump()
    {
        isJumping = true;
        while (isJumping)
        {
            float newY = Mathf.Sin(Time.time * jumpSpeed) * jumpHeight + originalPosition.y;
            // Clamp the Y position to ensure it doesn't go below the original position
            newY = Mathf.Max(newY, originalPosition.y);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }
    }
}
