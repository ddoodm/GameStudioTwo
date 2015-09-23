using UnityEngine;
using System.Collections;

public class persistentStats : MonoBehaviour {

	public Color playerColor;
	public Equipment[] playerItems;
	public Equipment[] boughtItems;
	public int playerMoney;


	// Update is called once per frame
	void Update () {

	}

    void Awake()
    {
		DontDestroyOnLoad(this);

		playerItems = new Equipment[5];
		for (int i = 0; i < 5; i++)
		{
			playerItems[i] = Equipment.EMPTY;
		}

		boughtItems = new Equipment[4];
		
		boughtItems[0] = Equipment.Item_Handle;
		boughtItems[1] = Equipment.Item_Spike;
		boughtItems[2] = Equipment.Item_Booster;
		boughtItems[3] = Equipment.EMPTY;


		playerMoney = 500;
	}
}
