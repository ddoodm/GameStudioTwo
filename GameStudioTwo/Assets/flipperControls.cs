using UnityEngine;
using System.Collections;

public class flipperControls : MonoBehaviour {

    public float force;
    HingeJoint hinge;

	// Use this for initialization
	void Start () {
        hinge = GetComponent<HingeJoint>();	
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            JointSpring hingeSpring = hinge.spring;
            hingeSpring.targetPosition = 135;
            hingeSpring.spring = force;
            hinge.spring = hingeSpring;
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            JointSpring hingeSpring = hinge.spring;
            hingeSpring.targetPosition = 0;
            hingeSpring.spring = 10f;
            hinge.spring = hingeSpring;
            
        }
	
	}
}
