using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SocketEquipment))]
public class FSMEquipmentController : MonoBehaviour
{
    private SocketEquipment socketEquipment;
    private Equipment[] equips;
    private GameObject player;

    public float
        flipperActivationRadius = 8.0f,
        flipperMinCooldownSeconds = 1.0f, flipperMaxCooldownSeconds = 2.8f;

	// Use this for initialization
	void Start ()
    {
        socketEquipment = GetComponent<SocketEquipment>();
        player = GameObject.FindWithTag("Player");
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
        }
    }

    private float flipperCooldownTimer = 0.0f;
    private void WeaponLogic_Flipper(SocketLocation socket)
    {
        flipperCooldownTimer -= Time.deltaTime;

        Weapon flipper = socketEquipment.GetWeaponInSocket(socket);
        Transform flipperTransform = flipper.GetGameObject().transform;

        float distanceToPlayer = (this.transform.position - player.transform.position).magnitude;

        bool playerInRange = distanceToPlayer <= flipperActivationRadius;
        bool imUpsideDown = Vector3.Dot(this.transform.root.up, Vector3.up) < 0;

        if ((playerInRange || imUpsideDown) && flipperCooldownTimer <= 0.0f)
        {
            flipper.Use();

            // The cooldown time should reflect the distance to the player:
            flipperCooldownTimer =
                Mathf.Lerp(
                    flipperMinCooldownSeconds,
                    flipperMaxCooldownSeconds,
                    1.0f - distanceToPlayer / flipperActivationRadius);
        }
    }
}
