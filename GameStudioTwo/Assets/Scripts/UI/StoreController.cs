using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public enum Equipment { EMPTY, Item_Handle, Item_Spike, Item_Flipper, Item_Booster};

public enum Socket { EMPTY, Socket_Left, Socket_Right, Socket_Front, Socket_Back, Socket_Top };

public enum StoreState {STATE_MODEL, STATE_ITEM, STATE_STORE};


public class StoreController : MonoBehaviour {

	public GameObject player;
	public GameObject storeUI;
	public GameObject uiPlayerModels;
	GameObject selectedModel;

	private Vector3 rgbColor = new Vector3(200f/255f, 50f/255f, 50f/255f);
	private Color sliderColour;
	public Color selectedItemColour = new Color(255f/255f, 200f/255f, 0f/255f);

	public StoreState current_state = StoreState.STATE_MODEL;

    persistentStats playerChoice;

	int noOfModels;
	int playerModelPos = 0;

	//bool hasSpike = false;
    bool colourChange = false;

	//
	// Selected items on the model
	//
	private static int MAX_SOCKETS = 5;
	private static int TOTAL_ITEMS = 4;
	private Equipment[] itemSocketArray = new Equipment[MAX_SOCKETS];
	public Equipment selectedEquipment = Equipment.EMPTY;
	private Socket selectedSocket = Socket.EMPTY;

	public Equipment[] AvailableItems = new Equipment[TOTAL_ITEMS];

	public int DOLLADOLLABILLSYALL = 0;
	public Text moneyText;

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
			itemSocketArray[i] = Equipment.EMPTY;
		}


		if (playerChoice != null){
			for (int i = 0; i < MAX_SOCKETS; i++)
			{
				playerChoice.playerItems[i] = itemSocketArray[i];
			}

			//get the bought items
			for (int i = 0; i < TOTAL_ITEMS; i++)
			{
				AvailableItems[i] = playerChoice.boughtItems[i];
			}

			DOLLADOLLABILLSYALL = playerChoice.playerMoney;

		}

		noOfModels = uiPlayerModels.GetComponent<Transform> ().childCount - 1;

		sliderColour = new Color(rgbColor.x, rgbColor.y, rgbColor.z);
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
						FillItemSocketArray();
					}

				}

				if (playerChoice != null){
					for (int i = 0; i < MAX_SOCKETS; i++)
					{
						playerChoice.playerItems[i] = itemSocketArray[i];
					}
				}
			}
		}


		HandleBoughtItems();

		moneyText.text = "Money: " + DOLLADOLLABILLSYALL;


        if (colourChange)
		{
			sliderColour = new Color(rgbColor.x, rgbColor.y, rgbColor.z);
			GameObject.FindGameObjectWithTag("Player Model").GetComponent<Renderer>().material.color = sliderColour;
			GameObject.FindGameObjectWithTag("Player Engine").GetComponent<Renderer>().material.color = sliderColour;
			colourChange = false;
        }

        if (playerChoice != null) 
		{
			playerChoice.playerColor = sliderColour;
		}
	}


	void HandleItemSelection(RaycastHit hit) {
		selectedSocket = Socket.EMPTY;

		switch (hit.transform.tag){
			case "LawnMowerRed":
			case "LawnMowerGreen":
			case "LawnMowerBlue":
				//player.GetComponent<Transform>().position = hit.transform.position;
				//hit.transform.parent.gameObject.SetActive(false);
				player.GetComponent<Transform>().position = new Vector3(7.5f, 2.6f, 7.5f);
				
				current_state = StoreState.STATE_ITEM;
				GetComponent<Animator>().SetTrigger("toItemSelection");
				break;
			case "Phone_Model":
				if (current_state == StoreState.STATE_ITEM)
				{
					GetComponent<Animator>().SetTrigger("toStoreSelection");
				current_state = StoreState.STATE_STORE;
				}
				break;

			case "Item_Handle":
				selectedEquipment = Equipment.Item_Handle;
				break;
				
			case "Item_Spike":
				selectedEquipment = Equipment.Item_Spike;
				break;
				
			case "Item_Flipper":
				selectedEquipment = Equipment.Item_Flipper;
				break;

			case "Item_Booster":
				selectedEquipment = Equipment.Item_Booster;
				break;

			case "Socket_Left":
				selectedSocket = Socket.Socket_Left;
				break;
				
			case "Socket_Right":
				selectedSocket = Socket.Socket_Right;
				break;
				
			case "Socket_Front":
				selectedSocket = Socket.Socket_Front;
				break;
				
			case "Socket_Back":
				selectedSocket = Socket.Socket_Back;
				break;
				
			case "Socket_Top":
				selectedSocket = Socket.Socket_Top;
				break;

			case "Player Model":
				break;

			default:
				selectedEquipment = Equipment.EMPTY;
				selectedSocket = Socket.EMPTY;
				break;
		}

		if (selectedEquipment == Equipment.Item_Handle && (selectedSocket != Socket.Socket_Back && selectedSocket != Socket.EMPTY)) {
			selectedSocket = Socket.EMPTY;
		}
		if (selectedEquipment == Equipment.Item_Spike && selectedSocket == Socket.Socket_Top) {
			selectedSocket = Socket.EMPTY;
		}
		if (selectedEquipment == Equipment.Item_Flipper && (selectedSocket == Socket.Socket_Top || selectedSocket == Socket.Socket_Back)) {
			selectedSocket = Socket.EMPTY;
		}

		if (selectedEquipment == Equipment.Item_Booster && (selectedSocket == Socket.Socket_Top || selectedSocket == Socket.Socket_Front)) {
			selectedSocket = Socket.EMPTY;
		}


	}


	// instantiate at sockets change rotation based on what socket. make this a separate script on the vehicle prefab
	
	void FillItemSocketArray(){
		switch (selectedSocket) {
			case Socket.Socket_Left:
				itemSocketArray[0] = selectedEquipment;
				break;

			case Socket.Socket_Right:
				itemSocketArray[1] = selectedEquipment;
				break;

			case Socket.Socket_Front:
				itemSocketArray[2] = selectedEquipment;
				break;

			case Socket.Socket_Back:
				itemSocketArray[3] = selectedEquipment;
				break;

			case Socket.Socket_Top:
				itemSocketArray[4] = selectedEquipment;
				break;

			default:
				selectedEquipment = Equipment.EMPTY;
				selectedSocket = Socket.EMPTY;
				return;

		}
		selectedEquipment = Equipment.EMPTY;
		selectedSocket = Socket.EMPTY;

		player.GetComponent<SocketEquipment>().SocketItems(itemSocketArray, false);

	}


	public void startTest(){
        if (playerChoice != null)
		{
			for (int i = 0; i < MAX_SOCKETS; i++)
			{
				playerChoice.playerItems[i] = itemSocketArray[i];
			}
			for (int i = 0; i < TOTAL_ITEMS; i++)
			{
				playerChoice.boughtItems[i] = AvailableItems[i];
			}



		}
        
        Application.LoadLevel(2);
	}


	public void HandleBoughtItems()
	{
		for (int i = 0; i < TOTAL_ITEMS; i++) 
		{
			switch (AvailableItems [i]) 
			{
				case Equipment.EMPTY:
					break;

				case Equipment.Item_Handle:
					break;


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

	public void BackToItems()
	{
		current_state = StoreState.STATE_ITEM;
		GetComponent<Animator>().SetTrigger("toItemSelection");
	}



	public void BuyFlipper()
	{
		Debug.Log (DOLLADOLLABILLSYALL);
		if (DOLLADOLLABILLSYALL > 100) {
			AvailableItems [2] = Equipment.Item_Flipper;
			DOLLADOLLABILLSYALL -= 100;
		}
	}

	public void BuyBooster()
	{
		if (DOLLADOLLABILLSYALL > 200) {
			AvailableItems [3] = Equipment.Item_Booster;
			DOLLADOLLABILLSYALL -= 200;
		}
	}


}
