using UnityEngine;
using System.Collections;

public class SocketEquipment : MonoBehaviour {
	
	public Transform player;

	public Transform prefab_handle;
	public Transform prefab_spike;
	public Transform prefab_flipper;
	
	public Transform socket_left;
	public Transform socket_right;
	public Transform socket_front;
	public Transform socket_back;
	public Transform socket_top;


	public Transform brace_left;
	public Transform brace_right;
	public Transform brace_front;

    private flipperControls temp;


	public void Start() {

	}


	public void SocketItems(Equipment[] equipmentArray, bool game){
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

				Transform handle_back = (Transform)Instantiate(prefab_handle, socket_back.position, Quaternion.identity);
				handle_back.Rotate(-90.0f, 0.0f, 0.0f, Space.World);
				handle_back.parent = socket_back;
				socket_back.transform.localPosition = new Vector3(-0.025f, 0.0f, 0.1f);

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

				brace_left.gameObject.SetActive(true);

				Transform spike_left = (Transform)Instantiate(prefab_spike, socket_left.position, Quaternion.identity);
				spike_left.Rotate(-90.0f, 180.0f, 90.0f, Space.World);
				spike_left.parent = socket_left;
				socket_left.transform.localPosition = new Vector3(0.0f, 0.19f, -0.045f);

				break;
				
			// Right Socket
			case 1:
				// Deactivate ball object
				foreach (Transform child in socket_right)
				{
					child.gameObject.SetActive(false);
				}
			
				brace_right.gameObject.SetActive(true);

				Transform spike_right = (Transform)Instantiate(prefab_spike, socket_right.position, Quaternion.identity);
				spike_right.Rotate(-90.0f, 0.0f, 90.0f, Space.World);
				spike_right.parent = socket_right;
				socket_right.transform.localPosition = new Vector3(0.0f, -0.19f, -0.04f);

				break;
				
			// Front Socket
			case 2:
				// Deactivate ball object
				foreach (Transform child in socket_front)
				{
					child.gameObject.SetActive(false);
				}
				
				brace_front.gameObject.SetActive(true);


				Transform spike_front = (Transform)Instantiate(prefab_spike, socket_front.position, Quaternion.identity);
				spike_front.Rotate(-90.0f, 0.0f, 0.0f, Space.World);
				spike_front.parent = socket_front;
				socket_front.transform.localPosition = new Vector3(-0.015f, 0.0f, -0.03f);

				break;
				
			// Back Socket
			case 3:
				// Deactivate ball object
				foreach (Transform child in socket_back)
				{
					child.gameObject.SetActive(false);
				}

				Transform spike_back = (Transform)Instantiate(prefab_spike, socket_back.position, Quaternion.identity);
				spike_back.Rotate(-90.0f, 0.0f, 180.0f, Space.World);
				spike_back.parent = socket_back;
				socket_back.transform.localPosition = new Vector3(0.2f, 0.0f, -0.07f);

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
			
				brace_left.gameObject.SetActive(true);

				Transform flipper_left = (Transform)Instantiate(prefab_flipper, socket_left.position, Quaternion.identity);
                temp = flipper_left.GetComponent<flipperControls>();
                if (temp != null)
                    temp.initialRot = new Vector3(0, 0, 0);
				flipper_left.Rotate(0.0f, 0.0f, 00.0f, Space.World);
				flipper_left.parent = socket_left;
				socket_left.transform.localPosition = new Vector3(0.0f, -0.678f, 0.3f);

				break;
				
			// Right Socket
			case 1:
				// Deactivate ball object
				foreach (Transform child in socket_right)
				{
					child.gameObject.SetActive(false);
				}
			
				brace_right.gameObject.SetActive(true);

				Transform flipper_right = (Transform)Instantiate(prefab_flipper, socket_right.position, Quaternion.identity);

                temp = flipper_right.GetComponent<flipperControls>();
                if (temp != null)
                    temp.initialRot = new Vector3(0, 180, 0);

				flipper_right.Rotate(0.0f, 180.0f, 0.0f, Space.World);
				flipper_right.parent = socket_right;
				socket_right.transform.localPosition = new Vector3(0.0f, 0.678f, 0.3f);
				break;
				
			// Front Socket
			case 2:
				// Deactivate ball object
				foreach (Transform child in socket_front)
				{
					child.gameObject.SetActive(false);
				}
			
				brace_front.gameObject.SetActive(true);

				Transform flipper_front = (Transform)Instantiate(prefab_flipper, socket_front.position, Quaternion.identity);
                
                temp = flipper_front.GetComponent<flipperControls>();
                if (temp != null)
                    temp.initialRot = new Vector3(0, 270, 0);
                
				flipper_front.Rotate(180.0f, -90.0f, 180.0f, Space.World);
				flipper_front.parent = socket_front;
				socket_front.transform.localPosition = new Vector3(1.23f, 0.0f, 0.185f);

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
