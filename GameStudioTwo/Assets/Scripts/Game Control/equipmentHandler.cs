using UnityEngine;
using System.Collections;

public class equipmentHandler : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        if (player.GetComponent<VehicleController>().player == 1)
        {
            if (GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>() != null)
            {
                persistentStats playerData = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();

                player.GetComponent<SocketEquipment>().SocketItems(playerData.playerItems, true);

                player.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);

                Renderer[] playerModel = player.GetComponentsInChildren<Renderer>();

                //GameObject[] playerModel = GameObject.FindGameObjectsWithTag("Player Model");
                foreach (Renderer part in playerModel)
                {
                    part.material.color = playerData.playerColor;
                }
            }
        }
        else if (player.GetComponent<VehicleController>().player == 2)
        {
            if (GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>() != null)
            {
                persistentStats playerData = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();

                player.GetComponent<SocketEquipment>().SocketItems(playerData.player2Items, true);

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
