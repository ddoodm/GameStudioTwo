using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public Transform target, player, enemy;

    public float
        minOrthoSize = 5.0f,
        velocityCoef = 2.5f,
        sizeAdjustSpeed = 0.8f;

    private Rigidbody targetBody;
    private Camera thisCamera;

    public bool velocityBased = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Transform>();

        if (target == null)
            target = player;

        thisCamera = this.GetComponent<Camera>();
        targetBody = target.GetComponent<Rigidbody>();
    }

	void LateUpdate ()
    {
		transform.position = new Vector3 (target.position.x,transform.position.y,target.position.z);

        float canDistanceTarget = velocityBased?
            targetBody.velocity.magnitude :
            (player.transform.position - enemy.transform.position).magnitude;

        if (targetBody)
            thisCamera.orthographicSize +=
                ((minOrthoSize + canDistanceTarget * velocityCoef)
                - thisCamera.orthographicSize)
                * sizeAdjustSpeed * Time.deltaTime;
	}
}
