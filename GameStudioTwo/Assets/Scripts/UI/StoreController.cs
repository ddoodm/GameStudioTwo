using UnityEngine;
using System.Collections;

public class StoreController : MonoBehaviour {

	GameObject player;
	GameObject uiPlayerModels;
	GameObject selectedModel;
	GameObject playerCamera;

	int noOfModels;
	int playerModelPos = 0;


	// Use this for initialization
	void Start () {
		//playerCamera.GetComponent<Transform> ().Rotate (30.0f, 0.0f, 0.0f);
		player = GameObject.FindGameObjectWithTag("Main Player");
		player.GetComponent<VehicleController>().play = false;
		player.GetComponent<Rigidbody>().useGravity = false;
		player.GetComponent<Transform>().position = new Vector3(0.0f, 5.0f, 0.0f);


		uiPlayerModels = GameObject.Find ("StoreUI/Models/Player");

		noOfModels = uiPlayerModels.GetComponent<Transform> ().childCount - 1;
		//selectedModel = GameObject.Find ("Lawn Mower");
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.tag == "LawnMower" || hit.transform.tag == "LawnMowerSmall" ||hit.transform.tag == "LawnMowerLarge")
				{
					player.GetComponentInChildren<Animator>().SetTrigger("FadeOutStore");
				}

				/*
				switch (hit.transform.tag)
				{
					case "LawnMower":
						
						break;
						
					case "LawnMowerSmall":
						
						break;
						
					case "LawnMowerLarge":
						
						break;
					
					default:
						break;
				}
				*/
			}
		}
	}


	public void MoveLeft() {
		if (playerModelPos > 0)
		{
			playerModelPos--;
			StartCoroutine ("SlideLeft");
		}
	}

	public void MoveRight() {
		if (playerModelPos < noOfModels)
		{
			playerModelPos++;
			StartCoroutine ("SlideRight");
		}
	}


	private IEnumerator SlideLeft(){
		Vector3 oldPos;
		for (int i = 0; i < 10; i++) 
		{
			oldPos = uiPlayerModels.GetComponent<Transform> ().position;
			uiPlayerModels.GetComponent<Transform>().position = new Vector3(oldPos.x + 1, oldPos.y, oldPos.z);
			yield return null;
		}
	}
	
	private IEnumerator SlideRight(){
		Vector3 oldPos;
		for (int i = 0; i < 10; i++) 
		{
			oldPos = uiPlayerModels.GetComponent<Transform> ().position;
			uiPlayerModels.GetComponent<Transform>().position = new Vector3(oldPos.x - 1, oldPos.y, oldPos.z);
			yield return null;
		}
	}




}
