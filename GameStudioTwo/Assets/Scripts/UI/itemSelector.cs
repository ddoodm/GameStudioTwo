using UnityEngine;
using System.Collections;

public class itemSelector : MonoBehaviour {

	private Color startColour;
	private Color highlightItemColour = new Color(50f/255f, 75f/255f, 255f/255f);
	private Color highlightSocketColour = new Color(0f/255f, 200f/255f, 0f/255f);
	private string currentSelection;
	public bool isSocket;
	private bool highlighted = false;

	private StoreController storeController;

	void Start()
	{
		storeController = GameObject.FindGameObjectWithTag("StoreUI").GetComponent<StoreController>();
		startColour = GetComponentInChildren<Renderer>().material.color;
	}

	void Update (){


		if (!highlighted) {
			if (GetComponent<Transform>().tag != storeController.selectedEquipment.ToString()|| GetComponent<Transform> ().tag == "Phone_Model") {
				if (!isSocket) {
					foreach (Transform child in transform) {
						child.GetComponent<Renderer> ().material.color = startColour;
					}
				} else {
					foreach (Transform child in transform) {
						if (child.GetComponent<Renderer> () != null){
							child.GetComponent<Renderer> ().material.color = startColour;
							child.GetComponent<Transform> ().localScale = new Vector3 (0.25f, 0.25f, 0.25f);
						}
					}
				}
			}
		}
	}


	
	void OnMouseEnter(){
		if (storeController.current_state != StoreState.STATE_ITEM)
			return;


		highlighted = true;
		if (GetComponent<Transform> ().tag != storeController.selectedEquipment.ToString() || GetComponent<Transform> ().tag == "Phone_Model" ) {
			if (!isSocket) {
				foreach (Transform child in transform) {
					child.GetComponent<Renderer> ().material.color = highlightItemColour;
				}
			} else {
				foreach (Transform child in transform) {
					if (child.GetComponent<Renderer> () != null){
						child.GetComponent<Renderer> ().material.color = highlightSocketColour;
						child.GetComponent<Transform> ().localScale = new Vector3 (0.375f, 0.375f, 0.375f);
					}
				}
			}
		} else {
			foreach (Transform child in transform) {
				child.GetComponent<Renderer> ().material.color = storeController.selectedItemColour;
			}
		}
	}
	
	
	void OnMouseExit(){
		highlighted = false;
		if (GetComponent<Transform> ().tag != storeController.selectedEquipment.ToString()|| GetComponent<Transform> ().tag == "Phone_Model") {
			if (!isSocket) {
				foreach (Transform child in transform) {
					child.GetComponent<Renderer> ().material.color = startColour;
				}
			} else {
				foreach (Transform child in transform) {
					if (child.GetComponent<Renderer> () != null){
						child.GetComponent<Renderer> ().material.color = startColour;
						child.GetComponent<Transform> ().localScale = new Vector3 (0.25f, 0.25f, 0.25f);
					}
				}
			}
		} else {
			foreach (Transform child in transform) {
				child.GetComponent<Renderer> ().material.color = storeController.selectedItemColour;
			}
		}
	}
}
