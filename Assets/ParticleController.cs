using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticle;
    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;
    [SerializeField] Rigidbody playerRb;

    private void Update()
    {
        if (Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if (!movementParticle.isPlaying)
            {
                movementParticle.Play();
            }
        }
        else
        {
            if (movementParticle.isPlaying)
            {
                movementParticle.Stop();
            }
        }
    }
}
