using UnityEngine;
using System.Collections;

public class SocketEquipment : MonoBehaviour {
	
	private Transform player;

	public Transform prefab_handle;
	public Transform prefab_spike;
	public Transform prefab_flipper;
	
	public Transform socket_left;
	public Transform socket_right;
	public Transform socket_front;
	public Transform socket_back;
	public Transform socket_top;



	public void Start() {
		player = GetComponent<Transform>();
	}


	public void SocketItems(Equipment[] equipmentArray){
		// Remove all items before putting more on
		RemoveItems();
		ResetSockets();


		for (int i = 0; i < 5; i++) {
			switch (equipmentArray [i]) {
				case Equipment.Item_Handle:
					SpawnHandle(i);
					break;
					
				case Equipment.Item_Spike:
					SpawnSpike(i);
					break;
					
				case Equipment.Item_Flipper:
					SpawnFlipper(i);
					break;

				default:
					break;
			}
		}

		// old code
		/*
		if (ItemSocketArray[2] == Equipment.EMPTY){
			GameObject spike = (GameObject)Instantiate(Resources.Load("Spike"), playerModel.transform.position, Quaternion.identity);
			spike.transform.Rotate(-180.0f, 0.0f, 0.0f, Space.World);
			spike.transform.localScale -= new Vector3(0.8f, 0.8f, 0.8f);
			spike.transform.parent = playerModel.transform;
		}
		*/
	}


	private void RemoveItems(){
		foreach (Transform child in socket_left){
			if (child.tag != "Socket_Ball") 
			{
				Destroy(child.gameObject);
			}
		}
		foreach (Transform child in socket_right){
			if (child.tag != "Socket_Ball")
			{
				Destroy(child.gameObject);
			}
		}
		foreach (Transform child in socket_front){
			if (child.tag != "Socket_Ball")
			{
				Destroy(child.gameObject);
			}
		}
		foreach (Transform child in socket_back){
			if (child.tag != "Socket_Ball") 
			{
				Destroy(child.gameObject);
			}
		}
		foreach (Transform child in socket_top){
			if (child.tag != "Socket_Ball") 
			{
				Destroy(child.gameObject);
			}
		}
	}


	private void ResetSockets(){
		socket_left.localPosition = new Vector3 (0.0f, -0.8f, 0.1f);
		socket_right.localPosition = new Vector3 (0.0f, 0.8f, 0.1f);
		socket_front.localPosition = new Vector3 (1.0f, 0.0f, 0.1f);
		socket_back.localPosition = new Vector3 (-0.85f, 0.0f, 0.1f);
		socket_top.localPosition = new Vector3 (0.0f, 0.0f, 0.7f);
	}




	private void SpawnHandle(int socket){
		switch (socket) {
			// Left Socket
			case 0:
				Debug.Log("Handle in wrong position");
				break;
				
			// Right Socket
			case 1:
				Debug.Log("Handle in wrong position");
				break;
				
			// Front Socket
			case 2:
				Debug.Log("Handle in wrong position");
				break;

			// Back Socket
			case 3:
				// Deactivate ball object
				foreach (Transform child in socket_back)
			{
					child.gameObject.SetActive(false);
				}

				Transform handle_back = (Transform)Instantiate(prefab_handle, player.position, Quaternion.identity);
				handle_back.Rotate(-180.0f, 0.0f, 0.0f, Space.World);
				handle_back.parent = socket_back;
				socket_back.transform.localPosition = new Vector3(-0.85f, 0.0f, 0.25f);

				break;
			
			// Top Socket
			case 4:			
				Debug.Log("Handle in wrong position");
				break;
					
			default:
				break;
		}
	}

	private void SpawnSpike(int socket){
		switch (socket) {
			// Left Socket
			case 0:
				// Deactivate ball object
				foreach (Transform child in socket_left)
				{
					child.gameObject.SetActive(false);
				}
			
				Transform spike_left = (Transform)Instantiate(prefab_spike, player.position, Quaternion.identity);
				spike_left.Rotate(0.0f, 180.0f, 90.0f, Space.World);
				spike_left.parent = socket_left;
				socket_left.transform.localPosition = new Vector3(0.0f, -0.2f, 0.08f);

				break;
				
			// Right Socket
			case 1:
				// Deactivate ball object
				foreach (Transform child in socket_right)
				{
					child.gameObject.SetActive(false);
				}
				
				Transform spike_right = (Transform)Instantiate(prefab_spike, player.position, Quaternion.identity);
				spike_right.Rotate(180.0f, 0.0f, 90.0f, Space.World);
				spike_right.parent = socket_right;
				socket_right.transform.localPosition = new Vector3(0.0f, 0.2f, 0.08f);

				break;
				
			// Front Socket
			case 2:
				// Deactivate ball object
				foreach (Transform child in socket_front)
				{
					child.gameObject.SetActive(false);
				}
			
				Transform spike_front = (Transform)Instantiate(prefab_spike, player.position, Quaternion.identity);
				spike_front.Rotate(180.0f, 0.0f, 0.0f, Space.World);
				spike_front.parent = socket_front;
				//socket_front.transform.localPosition = new Vector3(0.0f, 0.2f, 0.08f);

				break;
				
			// Back Socket
			case 3:
				// Deactivate ball object
				foreach (Transform child in socket_back)
				{
					child.gameObject.SetActive(false);
				}

				Transform spike_back = (Transform)Instantiate(prefab_spike, player.position, Quaternion.identity);
				spike_back.Rotate(0.0f, 180.0f, 0.0f, Space.World);
				spike_back.parent = socket_back;
				//socket_back.transform.localPosition = new Vector3(0.0f, 0.2f, 0.08f);

				break;
				
			// Top Socket
			case 4:
				Debug.Log("Spike in wrong position");

				break;
				
			default:
				break;
		}
	}

	private void SpawnFlipper(int socket){
		switch (socket) {
			// Left Socket
			case 0:
				// Deactivate ball object
				foreach (Transform child in socket_left)
				{
					child.gameObject.SetActive(false);
				}
			
				Transform flipper_left = (Transform)Instantiate(prefab_flipper, player.position, Quaternion.identity);
				flipper_left.Rotate(0.0f, 0.0f, -90.0f, Space.World);
				flipper_left.parent = socket_left;
				socket_left.transform.localPosition = new Vector3(-0.175f, -1.285f, 0.685f);

				break;
				
			// Right Socket
			case 1:
				// Deactivate ball object
				foreach (Transform child in socket_right)
				{
					child.gameObject.SetActive(false);
				}
			
				Transform flipper_right = (Transform)Instantiate(prefab_flipper, player.position, Quaternion.identity);
				flipper_right.Rotate(0.0f, 0.0f, 90.0f, Space.World);
				flipper_right.parent = socket_right;
				socket_right.transform.localPosition = new Vector3(0.175f, 1.285f, 0.685f);
				break;
				
			// Front Socket
			case 2:
				// Deactivate ball object
				foreach (Transform child in socket_front)
				{
					child.gameObject.SetActive(false);
				}
			
				Transform flipper_front = (Transform)Instantiate(prefab_flipper, player.position, Quaternion.identity);
				flipper_front.Rotate(0.0f, 0.0f, 180.0f, Space.World);
				flipper_front.parent = socket_front;
				socket_front.transform.localPosition = new Vector3(1.7f, -0.15f, 0.685f);

				break;
				
			// Back Socket
			case 3:
				Debug.Log("Flipper in wrong position");
				break;
				
			// Top Socket
			case 4:	
				Debug.Log("Flipper in wrong position");
				break;
				
			default:
				break;
		}
	}



}
