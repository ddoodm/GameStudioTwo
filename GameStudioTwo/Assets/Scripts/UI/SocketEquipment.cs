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
			if (child.tag != "Socket_Ball") {
				Destroy(child.gameObject);
			}
		}
		foreach (Transform child in socket_right){
			if (child.tag != "Socket_Ball") {
				Destroy(child.gameObject);
			}
		}
		foreach (Transform child in socket_front){
			if (child.tag != "Socket_Ball") {
				Destroy(child.gameObject);
			}
		}
		foreach (Transform child in socket_back){
			if (child.tag != "Socket_Ball") {
				Destroy(child.gameObject);
			}
		}
		foreach (Transform child in socket_top){
			if (child.tag != "Socket_Ball") {
				Destroy(child.gameObject);
			}
		}
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
				foreach (Transform child in socket_back){
					child.gameObject.SetActive(false);
				}

				Transform handle_back = (Transform)Instantiate(prefab_handle, player.position, Quaternion.identity);
				handle_back.Rotate(-180.0f, 0.0f, 0.0f, Space.World);
				handle_back.parent = socket_back;
				socket_back.transform.localPosition = new Vector3(socket_back.transform.localPosition.x, socket_back.transform.localPosition.y, socket_back.transform.localPosition.z  +.15f);

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

			break;
			
			// Right Socket
		case 1:

			break;
			
			// Front Socket
		case 2:

			break;
			
			// Back Socket
		case 3:
			// Deactivate ball object
			foreach (Transform child in socket_back){
				child.gameObject.SetActive(false);
			}

			
			break;
			
			// Top Socket
		case 4:			

			break;
			
		default:
			break;
		}
	}

	private void SpawnFlipper(int socket){
		switch (socket) {
			// Left Socket
		case 0:

			break;
			
			// Right Socket
		case 1:

			break;
			
			// Front Socket
		case 2:

			break;
			
			// Back Socket
		case 3:
			// Deactivate ball object
			foreach (Transform child in socket_back){
				child.gameObject.SetActive(false);
			}
			
			break;
			
			// Top Socket
		case 4:			

			break;
			
		default:
			break;
		}
	}



}
