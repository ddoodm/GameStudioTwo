using UnityEngine;
using System.Collections;

public class mainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void singlePlayer()
    {
        Application.LoadLevel("ItemStore");
    }

    public void multiPlayer()
    {
        Application.LoadLevel("MultiStore");
    }

    public void quit()
    {
        Application.Quit();
    }
}
