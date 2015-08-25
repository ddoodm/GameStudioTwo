using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour
{
    /// <summary>
    /// Wheel colliders at vehicle edges
    /// TODO: Create axle class
    /// </summary>
    public WheelCollider[] wheels;
	public bool play = true;
    public HPBar energyBar;

    /// <summary>
    /// Motor, braking and steering constraints
    /// </summary>
    public float
        maxTorque = 50.0f,
        maxBrakeTorque = 50.0f,
        maxEnergy = 50.0f,
        speedMultiplier = 1.0f,
        steeringAngle = 20.0f;

    /// <summary>
    /// Controller inputs obtained at frame update
    /// </summary>

    private bool
        boosting;

    private float
        inputLinearForce,
        inputSteering;

    private float _energy;

    public float energy
    {
        get { return _energy; }
        set
        {
            _energy = value < 0 ? 0 : value;

            if(energyBar)
                energyBar.value = _energy;
        }
    }

    void Start()
    {
    }

    void Update()
    {
		if (play) 
		{
			// Update input forces
            if (Input.GetKey(KeyCode.W) || Input.GetButton("Fire1"))
                inputLinearForce = 1;
            else if (Input.GetKey(KeyCode.S) || Input.GetButton("Fire2"))
                inputLinearForce = -1;
            else
                inputLinearForce = 0;

            if (Input.GetButtonDown("Boost") && energy >= 1)
            {
                boosting = true;
            }
            else if (Input.GetButtonUp("Boost"))
            {
                boosting = false;
            }
            boost();
            
			    //inputLinearForce = Input.GetAxis ("Vertical");
			inputSteering = Input.GetAxis ("Horizontal");
		}
    }

    void FixedUpdate()
	{
		if (play) {
			// Front-wheel steering
			float steerAngle = inputSteering * steeringAngle;
			wheels [0].steerAngle = wheels [1].steerAngle = steerAngle;

			// All-wheel drive
			float torque = inputLinearForce * maxTorque * speedMultiplier;
			for (int i = 0; i < wheels.Length; i++)
				wheels [i].motorTorque = torque;
		}
    }

    void boost()
    {
        if (energy > 0 && boosting)
        {
            energy -= 1;
            speedMultiplier = 5;
        }
        if (energy == 0)
        {
            speedMultiplier = 1;
            boosting = false;
        }
        if (boosting == false && energy < maxEnergy)
        {
            energy += 0.25f;
            speedMultiplier = 1;
        }

    }
}
