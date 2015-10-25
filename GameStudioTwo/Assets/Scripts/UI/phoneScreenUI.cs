using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class phoneScreenUI : MonoBehaviour {

    public Button button;
    public int price;
    public Equipment thisItem;
    StoreController storeController;
    private persistentStats stats;

	void Start () {
        storeController = GameObject.FindGameObjectWithTag("StoreUI").GetComponent<StoreController>();
        stats = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();
        if (storeController.AvailableItems[(int)thisItem] != Equipment.EMPTY)
        {
           button.interactable = false;
        }
    }
	

    public void DisableBuy()
    {
        if (storeController.money >= price)
        {
            button.interactable = false;
            storeController.money -= price;
            storeController.AvailableItems[(int)thisItem] = thisItem;
            stats.playerMoney -= price;

        }
    }










}
