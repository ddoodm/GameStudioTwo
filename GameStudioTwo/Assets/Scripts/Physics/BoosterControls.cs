using UnityEngine;
using System.Collections;

public class BoosterControls : MonoBehaviour {


    public KeyCode control;
    public string button;
    public Transform body;


    public int face;

    public float strafeSpeed;

    public bool thrust;



	// Use this for initialization
	void Start () {

        control = transform.parent.GetComponentInParent <PlayerSocketController>().control;
        button = transform.parent.GetComponentInParent<PlayerSocketController>().button;

        body = transform.parent.parent.parent.transform;

        switch (control)
        {
            case KeyCode.Alpha1: face = 1; break; //left
            case KeyCode.Alpha2: face = 2; break; //right
            case KeyCode.Alpha3: face = 3; break; //front
            case KeyCode.Alpha4: face = 4; break; //back
        }
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown(button) || Input.GetKeyDown(control))
        {
            switch(control)
            {
                case KeyCode.Alpha1:
                    {
                        if (body.GetComponent<VehicleController>().checkEnergyFull())
                        {
                            thrust = true;
                            StartCoroutine("strafeParticles");
                            body.GetComponent<Rigidbody>().AddForce(body.transform.right * strafeSpeed);
                            body.GetComponent<VehicleController>().drainEnergy();
                        }
                        break;
                        
                    }
                case KeyCode.Alpha2:
                    {
                        if (body.GetComponent<VehicleController>().checkEnergyFull())
                        {
                            thrust = true;
                            StartCoroutine("strafeParticles");
                            body.GetComponent<Rigidbody>().AddForce(-body.transform.right * strafeSpeed);
                            body.GetComponent<VehicleController>().drainEnergy();
                        }
                        break;
                    }
                case KeyCode.Alpha4:
                    {
                        thrust = true;
                        body.GetComponent<VehicleController>().boosting = true;
                        break;
                    }
            }
        }
        if (Input.GetButtonUp(button) || Input.GetKeyUp(control))
        {
            switch (control)
            {
                case KeyCode.Alpha4:
                    {
                        thrust = false;
                        body.GetComponent<VehicleController>().boosting = false;
                        break;
                    }

            }
        }
        thrustParticles();
	}

    private void thrustParticles()
    {
        if (thrust)
        {
            if (this.GetComponent<AudioSource>().isPlaying == false)
            {
                this.GetComponent<AudioSource>().volume = 0.3f;
                this.GetComponent<AudioSource>().Play();
                
            }
            this.GetComponentInChildren<ParticleEmitter>().minSize = 0.9f;
            this.GetComponentInChildren<ParticleEmitter>().maxSize = 0.95f;
            this.GetComponentInChildren<ParticleAnimator>().doesAnimateColor = true;
        }
        else
        {
            this.GetComponent<AudioSource>().volume -= 0.01f;
            this.GetComponentInChildren<ParticleEmitter>().minSize = 0.5f;
            this.GetComponentInChildren<ParticleEmitter>().maxSize = 0.55f;
            this.GetComponentInChildren<ParticleAnimator>().doesAnimateColor = false;
        }

    }



    IEnumerator strafeParticles()
    {
        yield return new WaitForSeconds(1.0f);
        thrust = false;
        StopCoroutine("strafeParticles");
    }

}
