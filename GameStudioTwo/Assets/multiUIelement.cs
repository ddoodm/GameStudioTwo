using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class multiUIelement : MonoBehaviour {

    public int player;
    public bool isUI;

    private Color startColour;
    private Color highlightItemColour = new Color(50f / 255f, 75f / 255f, 255f / 255f);
    private Color current;

	// Use this for initialization
	void Start () {
        if (isUI)
        {
            
            startColour = GetComponent<Image>().color;
            current = startColour;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (isUI)
            GetComponent<Image>().color = current;
	
	}

    public void swapColor()
    {
        if (current == startColour)
        {
            current = highlightItemColour;
        }
        else
        {
            current = startColour;
        }
    }
}
