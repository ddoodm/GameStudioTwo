using UnityEngine;
using System.Collections;
using System;

public class flipperControls : MonoBehaviour, Weapon {

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


    public float animationTime;
    public AnimationCurve curve;

	// Use this for initialization
	void Start () {

        control = transform.parent.GetComponentInParent<PlayerSocketController>().control;
        button = transform.parent.GetComponentInParent<PlayerSocketController>().button;

        thisRigidbody = GetComponent<Rigidbody>();
        opRigidbody = GameObject.FindWithTag("Enemy").GetComponent<Rigidbody>();

        canFlip = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        DoAnimation();
        DoAudio();
    }

    /// <summary>
    /// Implementation of Weapon.Use()
    /// </summary>
    public void Use()
    {
        if(canFlip)
            Flip();
    }

    private void Flip()
    {
        animationTime = 0;

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

        // Rob's code to flip the player
        else if (Vector3.Dot(transform.up, Vector3.up) < 0)
            transform.parent.GetComponentInParent<Rigidbody>().AddForceAtPosition(0.5f * Vector3.up * flipForce, this.transform.position, ForceMode.Impulse);
    }

    private void DoAnimation()
    {
        animationTime += Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, transform.parent.parent.parent.transform.rotation.eulerAngles.y - initialRot.y, curve.Evaluate(animationTime) * -180));
    }

    private void DoAudio()
    {
        if (animationTime >= 0 && GetComponent<AudioSource>().isPlaying == false && animationTime <= 1)
            GetComponent<AudioSource>().Play();
    }
}
