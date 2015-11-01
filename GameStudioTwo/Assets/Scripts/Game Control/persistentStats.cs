using UnityEngine;
using System.Collections;

public class persistentStats : MonoBehaviour {

	public Color playerColor;
	public Equipment[] playerItems;
	public Equipment[] boughtItems;
	public int playerMoney;
    public string model;

    public Equipment[] player2Items;
    public Color player2Color;


    public bool tutorialActive = true;


	// Update is called once per frame
	void Update () {

	}

    void Awake()
    {
        DontDestroyOnLoad(this);

        playerItems = new Equipment[5];
        player2Items = new Equipment[5];
        for (int i = 0; i < 5; i++)
        {
            playerItems[i] = Equipment.EMPTY;
            player2Items[i] = Equipment.EMPTY;
        }

        boughtItems = new Equipment[10];



        for (int i = 0; i < 10; i++)
        { 
            boughtItems[i] = Equipment.EMPTY;
        }
        playerMoney = 10000;

        /*
        boughtItems[0] = Equipment.Item_Handle;
        boughtItems[1] = Equipment.Item_BasicEngine;
        boughtItems[2] = Equipment.Item_Spike;
        boughtItems[3] = Equipment.Item_Flipper;
        boughtItems[4] = Equipment.Item_Booster;
        boughtItems[5] = Equipment.Item_MetalShield;
        boughtItems[6] = Equipment.Item_PlasmaShield;
        boughtItems[7] = Equipment.Item_CircularSaw;
        boughtItems[8] = Equipment.Item_Hammer;
        boughtItems[9] = Equipment.Item_TeslaCoil;*/

        model = "BaseMower";
	}
}
