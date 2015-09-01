using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreController : MonoBehaviour {

	GameObject player;
	GameObject playerModel;
	GameObject StoreUI;
	GameObject uiPlayerModels;
	GameObject selectedModel;
	GameObject playerCamera;

    public Vector3 rgbColor;


    persistentStats playerChoice;

	int noOfModels;
	int playerModelPos = 0;

	bool hasSpike = false;
    bool modelChosen = false;


	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        if (GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>() != null)
            playerChoice = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();

		//playerCamera.GetComponent<Transform> ().Rotate (30.0f, 0.0f, 0.0f);
		player = GameObject.FindGameObjectWithTag("Main Player");
		player.GetComponent<VehicleController>().play = false;
		player.GetComponent<Rigidbody>().useGravity = false;

		playerCamera = GameObject.FindGameObjectWithTag("Player Camera");
		//playerCamera.SetActive(false);
		playerModel = GameObject.FindGameObjectWithTag("Player Model");
		player.SetActive (false);


		StoreUI = GameObject.FindGameObjectWithTag("StoreUI");

		uiPlayerModels = GameObject.Find ("StoreUI/Models/Player");

		noOfModels = uiPlayerModels.GetComponent<Transform> ().childCount - 1;
		//selectedModel = GameObject.Find ("Lawn Mower");
	}


	// Update is called once per frame
	void Update () {
        Color sliderColor = new Color(rgbColor.x, rgbColor.y, rgbColor.z);
		if (Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.tag == "LawnMowerRed" || hit.transform.tag == "LawnMowerGreen" || hit.transform.tag == "LawnMowerBlue")
				{
					player.SetActive(true);
					Material selectedModel = hit.transform.gameObject.GetComponent<Renderer>().material;


					GameObject.FindGameObjectWithTag("Player Model").GetComponent<Renderer>().material.color = sliderColor;
					GameObject.FindGameObjectWithTag("Player Engine").GetComponent<Renderer>().material.color = sliderColor;


					GameObject.Find ("VehicleV2").GetComponent<Transform>().position = 
						(hit.transform.gameObject.GetComponent<Transform>().position);

					hit.transform.parent.gameObject.SetActive(false);

                    //Robs bad code
                    modelChosen = true;
                    //Ends here

					StoreUI.GetComponentInChildren<Animator>().SetTrigger("FadeOut");
				}


				if (hit.transform.tag == "Spike")
				{
					if (!hasSpike) 
					{
						hasSpike = true;
						//GameObject spike = (GameObject)Instantiate(Resources.Load("Spike"), new Vector3(0.2f, playerModel.transform.localPosition.y, 0.02f), playerModel.transform.rotation);
						GameObject spike = (GameObject)Instantiate(Resources.Load("Spike"), player.transform.position, Quaternion.identity);
						//spike.transform.position = new Vector3 (0.0f, 10.0f, 0.0f);
						//spike.transform.rotation = Quaternion.identity;
						spike.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.World);
						spike.transform.localScale -= new Vector3(0.8f, 0.8f, 0.8f);
						//spike.transform.localPosition = new Vector3 (0.0f, 10.0f, 0.0f);
						spike.transform.parent = playerModel.transform;
                        if (playerChoice != null)
                            playerChoice.spike = true;
					}
				}
			}
		}

        if (modelChosen)
        {
            GameObject.FindGameObjectWithTag("Player Model").GetComponent<Renderer>().material.color = sliderColor;
            GameObject.FindGameObjectWithTag("Player Engine").GetComponent<Renderer>().material.color = sliderColor;
        }

        if (playerChoice != null)
            playerChoice.color = sliderColor;
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
    }

    public void changeG(float slider)
    {
        rgbColor.y = slider / 255;
    }

    public void changeB(float slider)
    {
        rgbColor.z = slider / 255;
    }






}
