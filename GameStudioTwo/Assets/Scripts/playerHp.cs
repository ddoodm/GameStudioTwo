using UnityEngine;
using System.Collections;

public class playerHp : MonoBehaviour {
	public GameObject hpBar;
	public GameObject miniHpBar;
	private float maxHP = 100f;
	private float currentHp = 0f;

	// Use this for initialization
	void Start () {
		currentHp = maxHP;
		//test the HP bar 
		InvokeRepeating ("decreaseHP", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void decreaseHP(){
		//damage calculate will replace this code
		currentHp -= 2f; 

		// calculate the hpValue to scale the HP bar
		float hpValue = currentHp / maxHP;
		updateHpBar(hpValue);
	}

	void updateHpBar(float myHp){
		// myHp value have to be between 0-1
		hpBar.transform.localScale = new Vector3 (myHp,hpBar.transform.localScale.y,hpBar.transform.localScale.z);
		miniHpBar.transform.localScale = new Vector3 (myHp,miniHpBar.transform.localScale.y,miniHpBar.transform.localScale.z);
	}
}
