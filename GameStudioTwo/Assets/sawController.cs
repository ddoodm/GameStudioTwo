﻿using UnityEngine;
using System.Collections;

public class sawController : MonoBehaviour, Weapon {

    private float animationTime = 1;
    public AnimationCurve activeCurve;
    public AnimationCurve lower;
    public AnimationCurve raise;

    public bool canFlip;

    public Vector3 initialRot;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DoAnimation();

    }

    private void DoAnimation()
    {
        animationTime += Time.deltaTime;
        float animCurveValue = activeCurve.Evaluate(animationTime);
        this.transform.rotation = Quaternion.Euler(new Vector3(0, transform.root.rotation.eulerAngles.y - initialRot.y, -90 + animCurveValue * 90));

        // The player may flip once the animation is reset
        if (animationTime >= 1.0f)
            canFlip = true;
    }


    public void Use()
    {
        if (canFlip)
        {
            animationTime = 0;
            activeCurve = lower;
            this.GetComponentInChildren<sawSpinner>().spinSpeed = 4;
        }
    }

    public void EndUse()
    {
        animationTime = 0;
        activeCurve = raise;
        this.GetComponentInChildren<sawSpinner>().spinSpeed = 1;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

}