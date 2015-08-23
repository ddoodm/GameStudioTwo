using UnityEngine;
using System.Collections;

public class colorSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>() != null)
        {
            persistentStats choice = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();
            GameObject[] bot = GameObject.FindGameObjectsWithTag("Player Model");
            foreach (GameObject part in bot)
            {
                part.GetComponent<Renderer>().material = choice.color;
            }
            if (choice.spike == false)
            {
                GameObject.FindGameObjectWithTag("Weapon").SetActive(false);
            }
        }
    }
}
