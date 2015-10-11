﻿using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(SocketEquipment))]
[RequireComponent(typeof(FSMBotController))]
public class FSMEquipmentController : MonoBehaviour
{
    private SocketEquipment socketEquipment;
    private FSMBotController botControl;
    private Equipment[] equips;
    private GameObject player;
    private Rigidbody thisBody;

    public float
        flipperActivationRadius = 8.0f,
        flipperMinCooldownSeconds = 1.0f, flipperMaxCooldownSeconds = 2.8f;

	// Use this for initialization
	void Start ()
    {
        socketEquipment = GetComponent<SocketEquipment>();
        botControl = GetComponent<FSMBotController>();
        player = GameObject.FindWithTag("Player");
        thisBody = GetComponent<Rigidbody>();

        flipperCooldownTimers = new float[Enum.GetNames(typeof(SocketLocation)).Length];
	}
	
	// Update is called once per frame
	void Update ()
    {
        equips = socketEquipment.equipmentTypes;

        // Process logic for each socket
        for (int socket = 0; socket < equips.Length; socket++)
            DoLogicFor((SocketLocation)socket, equips[socket]);
	}

    private void DoLogicFor(SocketLocation socket, Equipment item)
    {
        switch(item)
        {
            case Equipment.Item_Flipper: WeaponLogic_Flipper(socket); break;
            case Equipment.Item_Booster:
                if(socket == SocketLocation.BACK)
                    WeaponLogic_RearBooster(socket);
                else
                    WeaponLogic_LateralBooster(socket);
                break;
        }
    }

    private float[] flipperCooldownTimers;
    private void WeaponLogic_Flipper(SocketLocation socket)
    {
        flipperCooldownTimers[(int)socket] -= Time.deltaTime;

        Weapon flipper = socketEquipment.GetWeaponInSocket(socket);
        Transform flipperTransform = flipper.GetGameObject().transform;

        float distanceToPlayer = (this.transform.position - player.transform.position).magnitude;

        bool playerInRange = distanceToPlayer <= flipperActivationRadius;
        bool imUpsideDown = Vector3.Dot(this.transform.root.up, Vector3.up) < 0;
        bool correctState = botControl.state == FSMBotController.FSMState.ARRIVE;
        bool cooldownOkay = flipperCooldownTimers[(int)socket] <= 0.0f;

        if ((playerInRange || imUpsideDown) && correctState && cooldownOkay)
        {
            flipper.Use();

            // The cooldown time should reflect the distance to the player:
            flipperCooldownTimers[(int)socket] =
                UnityEngine.Random.Range(flipperMinCooldownSeconds, flipperMaxCooldownSeconds);
        }
    }

    // Unified booster-wide timer
    private float boosterCooldown = 0.0f;
    private void WeaponLogic_RearBooster(SocketLocation socket)
    {
        boosterCooldown -= Time.deltaTime;
        Weapon booster = socketEquipment.GetWeaponInSocket(socket);

        float distanceToPlayer = (this.transform.position - player.transform.position).magnitude;

        bool inRange = distanceToPlayer > 3.0f && distanceToPlayer < 15.0f;
        bool notStuck = thisBody.velocity.magnitude > 4.0f;
        bool correctState = botControl.state == FSMBotController.FSMState.ARRIVE;

        if (inRange && notStuck && correctState)
            booster.Use();
        else
            booster.EndUse();
    }

    private void WeaponLogic_LateralBooster(SocketLocation socket)
    {
        boosterCooldown -= Time.deltaTime;
        Weapon booster = socketEquipment.GetWeaponInSocket(socket);

        Vector3 toPlayer = (player.transform.position - this.transform.position);
        float distanceToPlayer = toPlayer.magnitude;
        float lateralDistance =
            this.transform.InverseTransformPoint(player.transform.position).x;

        // Do we want to boost to attack the player?
        bool boostTowardPlayer =
            (socket == SocketLocation.LEFT && lateralDistance > 4.0f)
            || (socket == SocketLocation.RIGHT && lateralDistance < -4.0f);
        boostTowardPlayer &= botControl.state == FSMBotController.FSMState.ARRIVE;

        // Do we want to boost to get un-stuck?
        bool isStuck = thisBody.velocity.magnitude < 0.1f;

        // Boost toward the player whenever possible,
        // or try to get un-stuck
        if ((boostTowardPlayer || isStuck)
            && boosterCooldown < 0.0f)
        {
            booster.Use();
            boosterCooldown = UnityEngine.Random.Range(0.8f, 2.0f);
        }
        else
            booster.EndUse();
    }
}