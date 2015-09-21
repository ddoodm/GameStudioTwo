using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SocketEquipment))]
public class FSMEquipmentController : MonoBehaviour
{
    private SocketEquipment socketEquipment;
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
        player = GameObject.FindWithTag("Player");
        thisBody = GetComponent<Rigidbody>();
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

    private float boosterCooldown = 0.0f;
    private void WeaponLogic_RearBooster(SocketLocation socket)
    {
        boosterCooldown -= Time.deltaTime;
        Weapon booster = socketEquipment.GetWeaponInSocket(socket);

        float distanceToPlayer = (this.transform.position - player.transform.position).magnitude;

        bool inRange = distanceToPlayer > 3.0f && distanceToPlayer < 15.0f;
        bool notStuck = thisBody.velocity.magnitude > 4.0f;

        if (inRange && notStuck)
            booster.Use();
        else
            booster.EndUse();
    }

    private void WeaponLogic_LateralBooster(SocketLocation socket)
    {
        boosterCooldown -= Time.deltaTime;
        Weapon booster = socketEquipment.GetWeaponInSocket(socket);

        float distanceToPlayer = (this.transform.position - player.transform.position).magnitude;

        if (distanceToPlayer < 10.0f && boosterCooldown < 0.0f)
        {
            booster.Use();
            boosterCooldown = 2.0f;
        }
        else
            booster.EndUse();
    }
}
