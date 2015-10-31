using UnityEngine;
using System.Collections;

public class equipmentHandler : MonoBehaviour {

	public Transform player;

    // changed to START from AWAKE so socketEqupment script is called first. Not sure if this has broken other things
    void Start()
    {
        VehicleController playerVehicle = player.GetComponent<VehicleController>();

        if (playerVehicle.player == 1)
        {
            if (GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>() != null)
            {
                persistentStats playerData = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();

                player.GetComponent<SocketEquipment>().SocketItems(playerData.playerItems, playerData.model);

                player.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);

                Renderer[] playerModel = player.GetComponentsInChildren<Renderer>();

                //GameObject[] playerModel = GameObject.FindGameObjectsWithTag("Player Model");
                foreach (Renderer part in playerModel)
                {
                    part.material.color = playerData.playerColor;
                }
            }
        }
        else if (playerVehicle.player == 2)
        {
            if (GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>() != null)
            {
                persistentStats playerData = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();

                player.GetComponent<SocketEquipment>().SocketItems(playerData.player2Items, playerData.model);

                //player.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);

                Renderer[] playerModel = player.GetComponentsInChildren<Renderer>();

                //GameObject[] playerModel = GameObject.FindGameObjectsWithTag("Player Model");
                foreach (Renderer part in playerModel)
                {
                    part.material.color = playerData.player2Color;
                }
            }
        }
    }
}
