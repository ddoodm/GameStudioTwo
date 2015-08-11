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
        maxSteeringAngle = 20.0f;

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

        Vector3 targDir = targetWaypt - this.transform.position;
        float targetAngle = Mathf.Atan2(targDir.z, targDir.x) * Mathf.Rad2Deg - 90.0f;
        Debug.DrawRay(transform.position, targDir);
        inputLinearForce = 1.0f;
        inputSteering = -targetAngle - this.transform.rotation.eulerAngles.y;

        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);

        // Robert's modifications, modified
        if (path.corners.Length == 2 && Vector3.Dot(targDir, transform.forward) < 0)
        {
            inputLinearForce = 0.5f;
            inputSteering = 40.0f;
        }
    }

    void FixedUpdate()
    {
        // Front-wheel steering
        float steerAngle = inputSteering;
        wheels[0].steerAngle = wheels[1].steerAngle = steerAngle;

        // Front-wheel drive
        float torque = inputLinearForce * maxTorque;
        for (int i = 0; i < 2; i++)
            wheels[i].motorTorque = torque;
    }
}
