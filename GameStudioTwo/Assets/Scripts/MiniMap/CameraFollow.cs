using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public Transform target;

    public float
        minOrthoSize = 5.0f,
        velocityCoef = 2.5f,
        sizeAdjustSpeed = 0.8f;

    private Rigidbody targetBody;
    private Camera thisCamera;

    void Start()
    {
        if (target == null)
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        thisCamera = this.GetComponent<Camera>();
        targetBody = target.GetComponent<Rigidbody>();
    }

	void LateUpdate ()
    {
		transform.position = new Vector3 (target.position.x,transform.position.y,target.position.z);

        if (targetBody)
            thisCamera.orthographicSize +=
                ((minOrthoSize + targetBody.velocity.magnitude * velocityCoef)
                - thisCamera.orthographicSize)
                * sizeAdjustSpeed * Time.deltaTime;
	}
}
