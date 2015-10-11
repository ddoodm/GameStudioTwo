using UnityEngine;
using System.Collections;

public class activateHammer : MonoBehaviour , Weapon {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Use()
    {
        transform.GetComponentInChildren<hammerControls>().useHammer();
    }

    public void EndUse()
    {
        // Not needed for the flipper
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
