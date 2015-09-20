using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform target;

    void Start()
    {
        if (target == null)
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

	void LateUpdate ()
    {
		transform.position = new Vector3 (target.position.x,transform.position.y,target.position.z);
	}
}
