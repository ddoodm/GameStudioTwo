using UnityEngine;
using System.Collections;
using System;

public class PlasmaShieldController : MonoBehaviour, Weapon
{
    public GameObject plasmaBallPrefab, plasmaBallInstance;
    public float
        forceRadius = 10.0f,
        forceAmount = 10.0f,
        vehicleMultiplier = 3.0f;
    public AnimationCurve plasBallAnim;

    private bool _active = false;
    private float startTime;
    private float initialFalloffY;

    private bool active
    {
        get { return _active; }
        set
        {
            _active = value;
            startTime = Time.time;
        }
    }

    void Update()
    {
        EnergyController energyCtrl = transform.root.GetComponent<EnergyController>();
        if (energyCtrl.energy <= 1.0f)
            active = false;

        // Animate plasma ball
        animateBall();

        if (!active)
            return;

        // Add a force to every rigidbody in the scene
        deflectWorld();

        // Drain energy
        energyCtrl.energy -= 0.8f;
    }

    private void animateBall()
    {
        if (!plasmaBallInstance)
            return;

        Keyframe lastFrame = plasBallAnim[plasBallAnim.length - 1];
        float endOfAnim = lastFrame.time;

        float dt = Time.time - startTime;
        float animVal = plasBallAnim.Evaluate(active? (dt) : (endOfAnim - dt));

        // Apply the falloff Y to the shader
        plasmaBallInstance.GetComponent<Renderer>().material.SetFloat("_FalloffY", animVal);

        // Destroy ball if the deactivation animation is complete
        // TODO: Should make this cleaner
        if (!active && animVal == plasBallAnim[0].value)
        {
            if (plasmaBallInstance != null)
                GameObject.Destroy(plasmaBallInstance);
            plasmaBallInstance = null;
        }
    }

    private void deflectWorld()
    {
        // Find all colliders in the area
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, forceRadius);
        foreach(Collider c in colliders)
        {
            // Do not interact with static objects
            // Try self-rigidbody or root rigidbody
            Rigidbody colbody = null;
            if (!(colbody = c.GetComponent<Rigidbody>()))
                if (!(colbody = c.transform.root.GetComponent<Rigidbody>()))
                    continue;

            // Do not interact with yourself
            // TODO: Maybe to object comparison instead of tag comparison
            if (this.transform.root.tag == c.transform.root.tag)
                continue;

            // Increase force if the collider is a mower
            float finalForce = forceAmount;
            if (c.transform.root.tag == "Player" || c.transform.root.tag == "Enemy")
                finalForce *= vehicleMultiplier;

            // Add the force. Origin is slightly below the bot
            colbody.AddExplosionForce(
                forceAmount,
                this.transform.position - this.transform.up * 2.0f,
                forceRadius);
        }
    }

    public void Use()
    {
        active = true;

        // Instantiate plasma effect if it is not already there
        if (plasmaBallInstance == null)
            plasmaBallInstance = 
                (GameObject)GameObject.Instantiate(plasmaBallPrefab,
                this.transform.root.position,
                Quaternion.identity);

        // Link plasma ball to the mower
        plasmaBallInstance.transform.parent = this.transform.root;

        // Save initial falloff Y
        initialFalloffY = plasmaBallInstance.GetComponent<Renderer>().material.GetFloat("_FalloffY");
    }

    public void EndUse()
    {
        active = false;

        /*
        if (plasmaBallInstance != null)
            GameObject.Destroy(plasmaBallInstance);
        plasmaBallInstance = null;
        */
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
