using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class flipCounterController : MonoBehaviour {

    public Text player;
    public Text Enemy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (player.text != "" && Enemy.text != "")
        {
            if (int.Parse(player.text) < int.Parse(Enemy.text))
            {
                Enemy.text = "";
            }
            else
            {
                player.text = "";
            }
        }
	
	}
}
