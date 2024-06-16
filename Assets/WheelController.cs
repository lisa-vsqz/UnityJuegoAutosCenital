using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backRightTransform;
    [SerializeField] Transform backLeftTransform;

    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 20f;
   

    public float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    public float isEngineRunning;


    private void FixedUpdate()
    {

        currentAcceleration = acceleration * Input.GetAxis("Vertical");


        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakForce = breakingForce;
        }
        else
        {
            currentBreakForce = 0f;
        }
        //Acelerar
        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;
  
        
        
        //Frenar
        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;

        //Girar
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontRight.steerAngle = currentTurnAngle;
        frontLeft.steerAngle = currentTurnAngle;

        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(backRight, backRightTransform);
        UpdateWheel(backLeft, backLeftTransform);
        
    }

    public void UpdateWheel( WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);


        trans.position = position;
        trans.rotation = rotation;
    }



    
}
