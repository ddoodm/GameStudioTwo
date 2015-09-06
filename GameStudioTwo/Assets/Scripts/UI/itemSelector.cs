using UnityEngine;
using System.Collections;

public class itemSelector : MonoBehaviour {

	private Color startColour;
	private Color highlightItemColour = new Color(50f/255f, 75f/255f, 255f/255f);
	private Color highlightSocketColour = new Color(0f/255f, 200f/255f, 0f/255f);
	private string currentSelection, oldSelection;
	public bool childMaterials;
	private bool highlighted = false;

	private StoreController storeController;

	void Start()
	{
		storeController = GameObject.FindGameObjectWithTag("StoreUI").GetComponent<StoreController>();
		startColour = GetComponentInChildren<Renderer>().material.color;
		oldSelection = "Empty";
	}

	void Update (){
		switch (storeController.selectedEquipment) {
		case Equipment.EMPTY:
			currentSelection = "Empty";
			break;
			
		case Equipment.HANDLE:
			currentSelection = "Item_Handle";
			break;
			
		case Equipment.SPIKE:
			currentSelection = "Item_Spike";
			break;
			
		case Equipment.FLIPPER:
			currentSelection = "Item_Flipper";
			break;
			
		default:
			break;
		}

		if (!highlighted) {
			if (GetComponent<Transform> ().tag != currentSelection) {
				if (childMaterials) {
					foreach (Transform child in transform) {
						child.GetComponent<Renderer> ().material.color = startColour;
					}
				} else {
					GetComponent<Renderer> ().material.color = startColour;
					GetComponent<Transform> ().localScale = new Vector3 (0.05f, 0.05f, 0.05f);
				}
			}
		}
	}


	
	void OnMouseEnter(){
		highlighted = true;
		if (GetComponent<Transform> ().tag != currentSelection) {
			if (childMaterials) {
				foreach (Transform child in transform) {
					child.GetComponent<Renderer> ().material.color = highlightItemColour;
				}
			} else {
				GetComponent<Renderer> ().material.color = highlightSocketColour;
				GetComponent<Transform> ().localScale = new Vector3 (0.075f, 0.075f, 0.075f);
			}
		} else {
			foreach (Transform child in transform) {
				child.GetComponent<Renderer> ().material.color = storeController.selectedItemColour;
			}
		}
	}
	
	
	void OnMouseExit(){
		highlighted = false;
		if (GetComponent<Transform> ().tag != currentSelection) {
			if (childMaterials) {
				foreach (Transform child in transform) {
					child.GetComponent<Renderer> ().material.color = startColour;
				}
			} else {
				GetComponent<Renderer> ().material.color = startColour;
				GetComponent<Transform> ().localScale = new Vector3 (0.05f, 0.05f, 0.05f);
			}
		} else {
			foreach (Transform child in transform) {
				child.GetComponent<Renderer> ().material.color = storeController.selectedItemColour;
			}
		}
	}
}
