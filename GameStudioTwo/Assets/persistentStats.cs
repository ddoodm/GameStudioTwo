using UnityEngine;
using System.Collections;

public class persistentStats : MonoBehaviour {

    public Material color;
    public bool spike;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
