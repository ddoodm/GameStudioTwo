using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radar : MonoBehaviour {
	// Use this for initialization
	public GameObject radarObject;
	public GameObject borderOjbect;
	public float switchDistance;
	public Transform helpTransform;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (radarObject.transform.position, transform.position) > switchDistance) {
			//switch to borderObjects
			helpTransform.LookAt (radarObject.transform);
			borderOjbect.transform.position = transform.position + switchDistance * helpTransform.forward;
			borderOjbect.layer = LayerMask.NameToLayer ("Radar");
			radarObject.layer = LayerMask.NameToLayer ("Invisible");
		} else {
			// switch back to radarObject
			borderOjbect.layer = LayerMask.NameToLayer("Invisible");
			radarObject.layer = LayerMask.NameToLayer("Radar");
		}
	}
}
