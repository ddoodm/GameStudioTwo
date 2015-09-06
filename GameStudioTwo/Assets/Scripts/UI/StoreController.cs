using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public enum Equipment { EMPTY, HANDLE, SPIKE, FLIPPER };

public enum Socket { EMPTY, LEFT, RIGHT, FRONT, BACK, TOP };


public class StoreController : MonoBehaviour {

	public GameObject playerModel;
	public GameObject storeUI;
	public GameObject uiPlayerModels;
	GameObject selectedModel;

    private Vector3 rgbColor;
	private Color sliderColour;
	public Color selectedItemColour = new Color(255f/255f, 200f/255f, 0f/255f);


    persistentStats playerChoice;

	int noOfModels;
	int playerModelPos = 0;

	bool hasSpike = false;
    bool colourChange = false;

	//
	// Selected items on the model
	//
	public static int MAX_SOCKETS = 5;
	private Equipment[] ItemSocketArray = new Equipment[MAX_SOCKETS];
	public Equipment selectedEquipment = Equipment.EMPTY;
	private Socket selectedSocket = Socket.EMPTY;



	// Use this for initialization
	void Start () {
        Time.timeScale = 1;

        if (GameObject.FindGameObjectWithTag ("Persistent Stats").GetComponent<persistentStats> () != null) 
		{
			playerChoice = GameObject.FindGameObjectWithTag ("Persistent Stats").GetComponent<persistentStats> ();
		}


		// equipment initializers=
		for (int i = 0; i < MAX_SOCKETS; i++)
		{
			ItemSocketArray[i] = Equipment.EMPTY;
		}

		noOfModels = uiPlayerModels.GetComponent<Transform> ().childCount - 1;

		rgbColor = new Vector3(200f/255f, 50f/255f, 50f/255f);

		sliderColour = new Color(rgbColor.x, rgbColor.y, rgbColor.z);


		//old code
		/*
		player = GameObject.FindGameObjectWithTag("Main Player");
		player.GetComponent<VehicleController>().play = false;
		player.GetComponent<Rigidbody>().useGravity = false;

		playerCamera = GameObject.FindGameObjectWithTag("Player Camera");
		playerModel = GameObject.FindGameObjectWithTag("Player Model");
		player.SetActive (false);
		*/
	}


	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				HandleItemSelection(hit);

				if (selectedEquipment != Equipment.EMPTY){
					foreach (Transform child in hit.transform) {
						child.GetComponent<Renderer>().material.color = selectedItemColour;
					}
					if (selectedSocket != Socket.EMPTY){
						InstantiateItemAtSocket();
					}

				}

				if (playerChoice != null){
					System.Array.Copy(ItemSocketArray, playerChoice.playerItems, 5);
				}

				//old code
				/*
				if (hit.transform.tag == "LawnMowerRed" || hit.transform.tag == "LawnMowerGreen" || hit.transform.tag == "LawnMowerBlue")
				{

					playerModel.GetComponent<Transform>().position = hit.transform.position;
						
					hit.transform.parent.gameObject.SetActive(false);


					GetComponent<Animator>().SetTrigger("FadeOut");


					player.SetActive(true);
					Material selectedModel = hit.transform.gameObject.GetComponent<Renderer>().material;


					GameObject.FindGameObjectWithTag("Player Model").GetComponent<Renderer>().material.color = sliderColor;
					GameObject.FindGameObjectWithTag("Player Engine").GetComponent<Renderer>().material.color = sliderColor;


					player.GetComponent<Transform>().position = 
						(hit.transform.gameObject.GetComponent<Transform>().position);
				}


				if (hit.transform.tag == "Spike")
				{
					if (!hasSpike)
					{
						hasSpike = true;

						GameObject spike = (GameObject)Instantiate(Resources.Load("Spike"), playerModel.transform.position, Quaternion.identity);
						spike.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.World);
						spike.transform.localScale -= new Vector3(0.8f, 0.8f, 0.8f);
						spike.transform.parent = playerModel.transform;

                        if (playerChoice != null)
                            playerChoice.spike = true;
					}
				}
				*/
			}
		}


        if (colourChange)
		{
			sliderColour = new Color(rgbColor.x, rgbColor.y, rgbColor.z);
			playerModel.GetComponent<Renderer>().material.color = sliderColour;
			GameObject.FindGameObjectWithTag("Player Engine").GetComponent<Renderer>().material.color = sliderColour;
			colourChange = false;
        }

        if (playerChoice != null) 
		{
			playerChoice.playerColor = sliderColour;
		}
	}


	void HandleItemSelection(RaycastHit hit) {
		switch (hit.transform.tag){
			case "LawnMowerRed":
			case "LawnMowerGreen":
			case "LawnMowerBlue":
				playerModel.GetComponent<Transform>().position = hit.transform.position;
				hit.transform.parent.gameObject.SetActive(false);
				
				GetComponent<Animator>().SetTrigger("FadeOut");
				break;

			case "Item_Handle":
				selectedEquipment = Equipment.HANDLE;
				break;
				
			case "Item_Spike":
				selectedEquipment = Equipment.SPIKE;
				break;
				
			case "Item_Flipper":
				selectedEquipment = Equipment.FLIPPER;
				break;

			case "Socket_Left":
				selectedSocket = Socket.LEFT;
				break;
				
			case "Socket_Right":
				selectedSocket = Socket.RIGHT;
				break;
				
			case "Socket_Front":
				selectedSocket = Socket.FRONT;
				break;
				
			case "Socket_Back":
				selectedSocket = Socket.BACK;
				break;
				
			case "Socket_Top":
				selectedSocket = Socket.TOP;
				break;

			case "Player Model":
				break;

			default:
				selectedEquipment = Equipment.EMPTY;
				break;
		}
			


	}


	
	
	void InstantiateItemAtSocket(){
		switch (selectedSocket) {
		case Socket.LEFT:
			if (ItemSocketArray[0] == Equipment.EMPTY){







			}



			break;



		case Socket.RIGHT:




			break;



		case Socket.FRONT:
			if (ItemSocketArray[2] == Equipment.EMPTY){
				GameObject spike = (GameObject)Instantiate(Resources.Load("Spike"), playerModel.transform.position, Quaternion.identity);
				spike.transform.Rotate(-180.0f, 0.0f, 0.0f, Space.World);
				spike.transform.localScale -= new Vector3(0.8f, 0.8f, 0.8f);
				spike.transform.parent = playerModel.transform;
				ItemSocketArray[2] = Equipment.SPIKE;
				selectedEquipment = Equipment.EMPTY;
				selectedSocket = Socket.EMPTY;
			}



			break;



		case Socket.BACK:




			break;



		case Socket.TOP:
			break;
			
			
			
			
			
			
			
		}
	}


	public void startTest(){
        if (!hasSpike)
        {
            if (playerChoice != null)
                playerChoice.spike = false;
        }
        Application.LoadLevel(2);

        //Sorry Jesse
        /*
		StoreUI.GetComponentInChildren<Animator>().SetTrigger("FadeOut");
		playerCamera.SetActive(true);
		//GameObject.Find ("StoreUI").SetActive(false);
		player.GetComponent<VehicleController>().play = true;
		player.GetComponent<Rigidbody>().useGravity = true;
         * */
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

    public void changeR(float slider)
    {
        rgbColor.x = slider / 255;
		colourChange = true;
    }

    public void changeG(float slider)
    {
		rgbColor.y = slider / 255;
		colourChange = true;
    }

    public void changeB(float slider)
    {
		rgbColor.z = slider / 255;
		colourChange = true;
    }






}
