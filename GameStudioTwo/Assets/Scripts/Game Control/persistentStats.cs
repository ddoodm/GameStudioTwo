using UnityEngine;
using System.Collections;

public class persistentStats : MonoBehaviour {

	public Color playerColor;
	public Equipment[] playerItems;


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
	}
}
