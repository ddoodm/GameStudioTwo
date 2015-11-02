using UnityEngine;
using System.Collections;

public class SceneOpener : MonoBehaviour {

    public int sceneID = 0;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
    public void ChangeLevel()
    {
        Application.LoadLevel(sceneID);
    }

	// Update is called once per frame
	void Update ()
    {
	
	}
}
