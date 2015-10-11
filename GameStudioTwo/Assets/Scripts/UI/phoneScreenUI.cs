using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class phoneScreenUI : MonoBehaviour {

    public Button button;
    public int price;
    public Equipment thisItem;
    StoreController storeController;

	void Start () {
        storeController = GameObject.FindGameObjectWithTag("StoreUI").GetComponent<StoreController>();
        if (storeController.AvailableItems[(int)thisItem] != Equipment.EMPTY)
        {
           button.interactable = false;
        }
    }
	

    public void DisableBuy()
    {
        if (storeController.DOLLADOLLABILLSYALL > price)
        {
            button.interactable = false;
            storeController.DOLLADOLLABILLSYALL -= price;
            storeController.AvailableItems[(int)thisItem] = thisItem;
        }
    }










}
