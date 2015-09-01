using UnityEngine;
using System.Collections;

public class flipperControls : MonoBehaviour {

    public float
        flipForce = 800.0f,
        proximityRadius = 1.0f;

    private float
        initialSpring, initialSpringTarget;

    public bool canFlip { get; protected set; }

    HingeJoint hinge;

    Rigidbody thisRigidbody, opRigidbody;

	// Use this for initialization
	void Start () {
        hinge = GetComponent<HingeJoint>();
        thisRigidbody = GetComponent<Rigidbody>();
        opRigidbody = GameObject.FindWithTag("Enemy").GetComponent<Rigidbody>();

        initialSpring = hinge.spring.spring;
        initialSpringTarget = hinge.spring.targetPosition;

        canFlip = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1) && canFlip)
        {
            // Flip the flipper's hinge joint
            flipHinge();

            // If the flipper is close enough to the bot:
            if ((this.transform.position - opRigidbody.transform.position).magnitude < proximityRadius)
            {
                Vector3 normal = (opRigidbody.transform.position - this.transform.position).normalized;

                // Apply a reliable force to the bot rigidbody
                opRigidbody.AddForceAtPosition(
                    0.5f * (normal + Vector3.up) * flipForce,
                    this.transform.position,
                    ForceMode.Impulse);
            }
        }
    }

    private void flipHinge ()
    {
        JointSpring hingeSpring = hinge.spring;
        hingeSpring.spring = initialSpring;
        hingeSpring.targetPosition = 135;
        hinge.spring = hingeSpring;

        canFlip = false;

        StartCoroutine(unflipHinge(hingeSpring));
    }

    IEnumerator unflipHinge(JointSpring spring)
    {
        yield return new WaitForSeconds(1.0f);
        spring.spring = 2.0f;
        spring.targetPosition = initialSpringTarget;
        hinge.spring = spring;

        yield return new WaitForSeconds(1.0f);
        spring.spring = initialSpring;
        hinge.spring = spring;

        canFlip = true;
    }
}
