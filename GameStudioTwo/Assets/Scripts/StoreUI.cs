using UnityEngine;
using System.Collections;

public class StoreUI : MonoBehaviour {

	public GameObject player;
	public GameObject uiModels;
	public GameObject selectedModel;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Main Player");
		player.GetComponent<VehicleController>().play = false;
		player.GetComponent<Rigidbody>().useGravity = false;
		player.GetComponent<Transform>().position = new Vector3(0.0f, 5.0f, 0.0f);


		uiModels = GameObject.Find("Store UI/UI Models");
		uiModels.GetComponent<Transform>().position = new Vector3(5.0f, 5.0f, 0.0f);

		
		selectedModel = GameObject.Find ("BoxModel");
		selectedModel.GetComponent<Transform>().position = new Vector3(0.0f, 6.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
