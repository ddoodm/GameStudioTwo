using UnityEngine;
using System.Collections;

public class CameraVehicleTrack : MonoBehaviour
{
    public Transform target;
    public float
        butter = 0.1f,
        velocityContribution = 0.2f;

    private Rigidbody targetBody;
    private Vector3 offset;
    private Vector3 offsetRot;

	void Start ()
    {
        offset = target.position - transform.position;
        offsetRot = transform.rotation.eulerAngles;
        targetBody = target.gameObject.GetComponent<Rigidbody>();

        offset.y *= -1.0f;
    }
	
	void FixedUpdate ()
    {
        transform.rotation = Quaternion.identity;

        Vector3 t = target.position;
        if (targetBody)
            t -= targetBody.velocity * velocityContribution;

        Vector3 dampPlayerPos = (transform.position - t) * butter * Time.deltaTime;

        transform.Translate(-dampPlayerPos);
        transform.Rotate(target.up, target.rotation.eulerAngles.y);
        transform.Translate(offset * butter * Time.deltaTime);
        transform.LookAt(target.position);

        transform.Rotate(-Vector3.Scale(offsetRot, Vector3.one - Vector3.up));

        transform.position = avoidFloor(transform.position);
        //transform.position = calcAvoidancePosition(transform.position);
    }

    /// <summary>
    /// Avoids static objects using the NavMesh.
    /// TODO: Make this better by fixing the camera sticking issue
    /// </summary>
    Vector3 calcAvoidancePosition(Vector3 inPos)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(inPos, out hit, 100.0f, NavMesh.AllAreas))
        {
            return new Vector3(hit.position.x, inPos.y, hit.position.z);
        }
        return Vector3.zero;
    }

    /// <summary>
    /// Avoids terrain / static floor by casting a ray down, and obtaining the distance
    /// </summary>
    Vector3 avoidFloor(Vector3 inPos)
    {
        RaycastHit hit;
        Ray r = new Ray(inPos, Vector3.down);
        float terrainDistance = 0.0f;
        if (Physics.Raycast(r, out hit, 1.0f))
            terrainDistance = hit.distance;

        return new Vector3(
            inPos.x,
            Mathf.Max(terrainDistance + 0.1f, inPos.y),
            inPos.z);
    }
}
