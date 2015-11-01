using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class phoneScreenUI : MonoBehaviour {

    public Button button;
    public Text buyText;
    public int price;
    public Equipment thisItem;
    StoreController storeController;
    private persistentStats stats;

    Color activeColour = new Color(0 / 255f, 225 / 255f, 30 / 255f);
    Color inactiveColour = new Color(125 / 255f, 200 / 255f, 125 / 255f);


    void Start () {
        storeController = GameObject.FindGameObjectWithTag("StoreUI").GetComponent<StoreController>();
        stats = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();

        buyText.text = "$" + price;
        if (storeController.AvailableItems[(int)thisItem] != Equipment.EMPTY)
        {
           button.interactable = false;
           button.image.color = inactiveColour;
        }
        else if (storeController.money >= price)
        {
            button.interactable = true;
            button.image.color = activeColour;
        }
    }


    void Update ()
    {
        if(storeController.AvailableItems[(int)thisItem] != Equipment.EMPTY)
        {
            button.interactable = false;
            button.image.color = inactiveColour;
        }
        else if (storeController.money >= price)
        {
            button.interactable = true;
            button.image.color = activeColour;
        }
    }
	

    public void DisableBuy()
    {
        if (storeController.money >= price)
        {
            button.interactable = false;
            button.image.color = inactiveColour;
            storeController.money -= price;
            storeController.AvailableItems[(int)thisItem] = thisItem;
            stats.playerMoney -= price;

        }
    }










}
