using UnityEngine;
using System.Collections;
using System;

public class BoosterControls : MonoBehaviour, Weapon
{
    SocketLocation location;

    public float strafeSpeed;

    public bool
        thrusting,
        forwardBoosting;

    // From Weapon interface
    public void Use()
    {
        Boost();
    }

    // From Weapon interface
    public void EndUse()
    {
        EndBoost();
    }

    // From Weapon interface
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    // Use this for initialization
    void Start ()
    {
        switch(transform.parent.name)
        {
            case "LeftSocket": location = SocketLocation.LEFT; break;
            case "RightSocket": location = SocketLocation.RIGHT; break;
            case "BackSocket": location = SocketLocation.BACK; break;
        }
	}

    private void Boost()
    {
        switch(location)
        {
            case SocketLocation.LEFT:
                thrusting = true;
                StartCoroutine("strafeParticles");
                transform.root.GetComponent<Rigidbody>().AddForce(transform.root.right * strafeSpeed);
                transform.root.GetComponent<EnergyController>().DrainEnergy();
                break;
            case SocketLocation.RIGHT:
                thrusting = true;
                StartCoroutine("strafeParticles");
                transform.root.GetComponent<Rigidbody>().AddForce(-transform.root.right * strafeSpeed);
                transform.root.GetComponent<EnergyController>().DrainEnergy();
                break;
            case SocketLocation.BACK:
                thrusting = true;
                forwardBoosting = true;
                //transform.root.GetComponent<VehicleController>().boosting = true;
                break;
        }
    }

    private void EndBoost()
    {
        if(location == SocketLocation.BACK)
        {
            thrusting = false;
            forwardBoosting = false;
            //transform.root.GetComponent<VehicleController>().boosting = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*
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
        */

        EnergyController energyCtrl = transform.root.GetComponent<EnergyController>();
        VehicleController vehicle = transform.root.GetComponent<VehicleController>();
        float energy = energyCtrl.energy;
        float maxEnergy = energyCtrl.maxEnergy;

        if (energy > 0 && forwardBoosting)
        {
            energy -= 1;
            vehicle.speedMultiplier = 5;
        }
        if (energy == 0)
        {
            vehicle.speedMultiplier = 1;
            forwardBoosting = false;
        }
        if (!forwardBoosting && energy < maxEnergy)
        {
            energy += 0.25f;
            vehicle.speedMultiplier = 1;
        }

        thrustParticles();
	}

    private void thrustParticles()
    {
        if (thrusting)
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
        thrusting = false;
        StopCoroutine("strafeParticles");
    }
}
