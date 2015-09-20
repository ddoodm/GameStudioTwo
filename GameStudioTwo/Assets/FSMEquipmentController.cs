using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SocketEquipment))]
public class FSMEquipmentController : MonoBehaviour
{
    private SocketEquipment socketEquipment;
    private Equipment[] equips;

	// Use this for initialization
	void Start ()
    {
        socketEquipment = GetComponent<SocketEquipment>();
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

    private void WeaponLogic_Flipper(SocketLocation socket)
    {
        Weapon flipper = socketEquipment.GetWeaponInSocket(socket);

        if (Time.time % 2.0f == 0)
            flipper.Use();
    }
}
