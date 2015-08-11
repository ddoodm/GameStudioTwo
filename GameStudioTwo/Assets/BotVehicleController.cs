using UnityEngine;
using System.Collections;

public class BotVehicleController : MonoBehaviour
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

    public Transform target;

    private NavMeshPath path;

    void Start()
    {
        path = new NavMeshPath();
    }

    void Update()
    {
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);

        Vector3 targetWaypt = path.corners.Length>1? path.corners[1] : target.position;
        Debug.DrawLine(targetWaypt, targetWaypt + Vector3.up * 10.0f, Color.green);

        Vector3 targDir = targetWaypt - transform.position;
        Debug.DrawRay(transform.position, targDir);
        inputLinearForce = 1.0f;
        inputSteering = -Mathf.Atan2(targDir.z, targDir.x) * Mathf.Rad2Deg + 90.0f;
        if (inputSteering < 0)
            inputSteering += 360.0f;

        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    }

    void FixedUpdate()
    {
        // Front-wheel steering
        float steerAngle = inputSteering;
        wheels[0].steerAngle = wheels[1].steerAngle = steerAngle;

        // Rear-wheel drive
        float torque = inputLinearForce * maxTorque;
        for (int i = 2; i < 4; i++)
            wheels[i].motorTorque = torque;
    }
}
