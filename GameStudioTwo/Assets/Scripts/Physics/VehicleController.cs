using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour
{
    /// <summary>
    /// Wheel colliders at vehicle edges
    /// TODO: Create axle class
    /// </summary>
    public WheelCollider[] wheels;

    /// <summary>
    /// Motor, braking and steering constraints
    /// </summary>
    public float
        maxTorque = 50.0f,
        maxBrakeTorque = 50.0f,
        steeringAngle = 20.0f;

    /// <summary>
    /// Controller inputs obtained at frame update
    /// </summary>
    private float
        inputLinearForce,
        inputSteering;

    void Update()
    {
        // Update input forces
        inputLinearForce = Input.GetAxis("Vertical");
        inputSteering = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // Front-wheel steering
        float steerAngle = inputSteering * steeringAngle;
        wheels[0].steerAngle = wheels[1].steerAngle = steerAngle;

        // All-wheel drive
        float torque = inputLinearForce * maxTorque;
        for (int i = 0; i < wheels.Length; i++)
            wheels[i].motorTorque = torque;
    }
}
