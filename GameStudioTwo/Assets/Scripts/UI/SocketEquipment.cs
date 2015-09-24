using UnityEngine;
using System.Collections;

public enum SocketLocation
{
    LEFT = 0, RIGHT, FRONT, BACK, TOP, NONE
}

public class SocketEquipment : MonoBehaviour {
	
	public Transform player;

	public Transform prefab_handle;
	public Transform prefab_spike;
	public Transform prefab_flipper;
	public Transform prefab_booster;
	
	public Transform socket_left;
	public Transform socket_right;
	public Transform socket_front;
	public Transform socket_back;
	public Transform socket_top;

	public Transform brace_left;
	public Transform brace_right;
	public Transform brace_front;

    public bool inStore;

    /// <summary>
    /// Each index corresponds to a SocketPosition.
    /// Equipment denotes the type that is held by a socket, not the weapon itself.
    /// </summary>
    public Equipment[] equipmentTypes { get; private set; }

    /// <summary>
    /// Each index corresponds to a SocketPosition.
    /// The actual references to the weapons contained by the sockets.
    /// NOTE: This array stores only equipment that implements the Weapon interface
    /// (eg, a flipper)
    /// </summary>
    public Weapon[] equipmentRefs { get; private set; }

    private flipperControls temp;

    public Weapon GetWeaponInSocket(SocketLocation socket)
    {
        return equipmentRefs[(int)socket];
    }

    private void AddWeaponReference(Transform itemTrans, SocketLocation socket)
    {
        if (!inStore)
        {
            Weapon weapon = itemTrans.GetComponent<Weapon>();
            if (weapon == null) throw new System.Exception(itemTrans.name + " has no component that implements Weapon.");
            equipmentRefs[(int)socket] = weapon;
        }
    }

	public void SocketItems(Equipment[] equipmentArray, bool game){
		// Remove all items before putting more on
		RemoveItems();
		ResetSockets();

        this.equipmentTypes = equipmentArray;
        this.equipmentRefs = new Weapon[equipmentTypes.Length];

		for (int i = 0; i < 5; i++) {
            SocketLocation iLocation = (SocketLocation)i;
            switch (equipmentArray [i]) {
				case Equipment.Item_Handle:
					SpawnHandle(iLocation);
					break;
					
				case Equipment.Item_Spike:
					SpawnSpike(iLocation);
					break;
					
				case Equipment.Item_Flipper:
					SpawnFlipper(iLocation);
					break;

				case Equipment.Item_Booster:
					SpawnBooster(iLocation);
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

	private void SpawnHandle(SocketLocation socket){
		switch (socket) {
			// Left Socket
			case SocketLocation.LEFT:
				Debug.Log("Handle in wrong position");
                equipmentTypes[(int)socket] = Equipment.EMPTY;
				break;
				
			// Right Socket
			case SocketLocation.RIGHT:
				Debug.Log("Handle in wrong position");
                equipmentTypes[(int)socket] = Equipment.EMPTY;
                break;
				
			// Front Socket
			case SocketLocation.FRONT:
				Debug.Log("Handle in wrong position");
                equipmentTypes[(int)socket] = Equipment.EMPTY;
                break;

			// Back Socket
			case SocketLocation.BACK:
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
			case SocketLocation.TOP:			
				Debug.Log("Handle in wrong position");
                equipmentTypes[(int)socket] = Equipment.EMPTY;
                break;
					
			default:
				break;
		}
	}

	private void SpawnSpike(SocketLocation socket){
		switch (socket) {
			// Left Socket
			case SocketLocation.LEFT:
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
			case SocketLocation.RIGHT:
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
			case SocketLocation.FRONT:
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
			case SocketLocation.BACK:
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
			case SocketLocation.TOP:
				Debug.Log("Spike in wrong position");
                equipmentTypes[(int)socket] = Equipment.EMPTY;
                break;
				
			default:
				break;
		}
	}

	private void SpawnFlipper(SocketLocation socket){
		switch (socket) {
			// Left Socket
			case SocketLocation.LEFT:
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
				flipper_left.Rotate(0.0f, 0.0f, 0.0f, Space.World);
				flipper_left.parent = socket_left;
				socket_left.transform.localPosition = new Vector3(0.0f, -0.678f, 0.3f);

                // Add a reference to the array of references
                AddWeaponReference(flipper_left, socket);

                break;
				
			// Right Socket
			case SocketLocation.RIGHT:
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

                // Add a reference to the array of references
                AddWeaponReference(flipper_right, socket);

                break;
				
			// Front Socket
			case SocketLocation.FRONT:
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

                // Add a reference to the array of references
                AddWeaponReference(flipper_front, socket);

                break;
				
			// Back Socket
			case SocketLocation.BACK:
				Debug.Log("Flipper in wrong position");
                equipmentTypes[(int)socket] = Equipment.EMPTY;
                break;
				
			// Top Socket
			case SocketLocation.TOP:	
				Debug.Log("Flipper in wrong position");
                equipmentTypes[(int)socket] = Equipment.EMPTY;
                break;
				
			default:
				break;
		}
	}

	private void SpawnBooster(SocketLocation socket){
		switch (socket) {
			// Left Socket
		case SocketLocation.LEFT:
			// Deactivate ball object
			foreach (Transform child in socket_left)
			{
				child.gameObject.SetActive(false);
			}
			
			brace_left.gameObject.SetActive(true);
			
			Transform booster_left = (Transform)Instantiate(prefab_booster, socket_left.position, Quaternion.identity);
			booster_left.Rotate(0.0f, 90.0f, 0.0f, Space.World);
			booster_left.parent = socket_left;
			socket_left.transform.localPosition = new Vector3(0.0f, -0.85f, 0.1f);

                AddWeaponReference(booster_left, socket);

                break;
			
			// Right Socket
		case SocketLocation.RIGHT:
			// Deactivate ball object
			foreach (Transform child in socket_right)
			{
				child.gameObject.SetActive(false);
			}
			
			brace_right.gameObject.SetActive(true);
			
			Transform booster_right = (Transform)Instantiate(prefab_booster, socket_right.position, Quaternion.identity);
			booster_right.Rotate(0.0f, -90.0f, 0.0f, Space.World);
			booster_right.parent = socket_right;
			socket_right.transform.localPosition = new Vector3(0.0f, 0.85f, 0.1f);

                AddWeaponReference(booster_right, socket);

                break;
			
			// Front Socket
		case SocketLocation.FRONT:
			Debug.Log("Booster in wrong position");
                equipmentTypes[(int)socket] = Equipment.EMPTY;
                break;

			// Back Socket
		case SocketLocation.BACK:
			// Deactivate ball object
			foreach (Transform child in socket_back)
			{
				child.gameObject.SetActive(false);
			}
			
			Transform booster_back = (Transform)Instantiate(prefab_booster, socket_back.position, Quaternion.identity);
			booster_back.Rotate(0.0f, 0.0f, 0.0f, Space.World);
			booster_back.parent = socket_back;
			socket_back.transform.localPosition = new Vector3(-0.775f, 0.0f, 0.15f);

                AddWeaponReference(booster_back, socket);

                break;
			
			// Top Socket
		case SocketLocation.TOP:	
			Debug.Log("Booster in wrong position");
                equipmentTypes[(int)socket] = Equipment.EMPTY;
                break;
			
		default:
			break;
		}
	}

}
