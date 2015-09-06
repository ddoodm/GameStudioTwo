using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	float speed = 75.0f;


	void FixedUpdate () {
			transform.Rotate (new Vector3(0.0f, 0.0f, 1.0f), speed * Time.deltaTime);
	}
}
