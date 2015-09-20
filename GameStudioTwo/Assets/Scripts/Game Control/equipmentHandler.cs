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
        if (GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>() != null)
        {
			persistentStats playerData = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();

			player.GetComponent<SocketEquipment>().SocketItems(playerData.playerItems, true);
			
			player.transform.Rotate (0.0f, 180.0f, 0.0f, Space.World);

            GameObject[] playerModel = GameObject.FindGameObjectsWithTag("Player Model");
            foreach (GameObject part in playerModel)
            {
                part.GetComponent<Renderer>().material.color = playerData.playerColor;
            }
        }
    }
}
