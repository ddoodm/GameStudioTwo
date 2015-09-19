using UnityEngine;
using System.Collections;

public class flipperControls : MonoBehaviour {

    public float
        flipForce = 800.0f,
        proximityRadius = 1.0f;

    private float
        initialSpring, initialSpringTarget, initialRotation;

    public Vector3 initialRot = new Vector3(0,0,0);

    public bool canFlip { get; protected set; }


    Rigidbody thisRigidbody, opRigidbody;
    public KeyCode control;
    public string button;


    public float curveVar;
    public AnimationCurve curve;

	// Use this for initialization
	void Start () {

        control = transform.parent.GetComponentInParent<socketControl>().control;
        button = transform.parent.GetComponentInParent<socketControl>().button;

        thisRigidbody = GetComponent<Rigidbody>();
        opRigidbody = GameObject.FindWithTag("Enemy").GetComponent<Rigidbody>();


        canFlip = true;
        curveVar = 1.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        curveVar += Time.deltaTime;
        if ((Input.GetButtonUp(button) || Input.GetKeyUp(control)) && curveVar > 1)
        {
            curveVar = 0;
            // Flip the flipper's hinge joint

            // The target may not have been collided with
            if (!opRigidbody)
                return;

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
            else if(Vector3.Dot(transform.up, Vector3.up) < 0)
            {
                transform.parent.GetComponentInParent<Rigidbody>().AddForceAtPosition(0.5f * Vector3.up * flipForce, this.transform.position, ForceMode.Impulse);
            }
            
        }
        this.transform.rotation = Quaternion.Euler(new Vector3(0, transform.parent.parent.parent.transform.rotation.eulerAngles.y - initialRot.y, curve.Evaluate(curveVar) * -180));
    }

}
