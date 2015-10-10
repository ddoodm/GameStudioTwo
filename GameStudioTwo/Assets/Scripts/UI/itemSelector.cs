using UnityEngine;
using System.Collections;

public class itemSelector : MonoBehaviour
{

    private Color startColour;
    private Color highlightItemColour = new Color(50f / 255f, 75f / 255f, 255f / 255f);
    private Color highlightSocketColour = new Color(0f / 255f, 200f / 255f, 0f / 255f);
    private string currentSelection;
    public bool isSocket;
    private bool highlighted = false;

    private StoreController storeController;

    public string objectName;
    

    
    void Start()
    {
        storeController = GameObject.FindGameObjectWithTag("StoreUI").GetComponent<StoreController>();
        startColour = GetComponentInChildren<Renderer>().material.color;
    }


    void Update()
    {
        currentSelection = storeController.selectedEquipment.ToString();

        if (highlighted)
        {
            return;
        }


        if (GetComponent<Transform>().tag != currentSelection)
        {
            foreach (Transform child in transform)
            {

                ResetColour(child);
            }
        }
    }
    

    void OnMouseEnter()
    {
        if (storeController.current_state != StoreState.STATE_ITEM)
        {
            return;
        }

        highlighted = true;

        if (GetComponent<Transform>().tag != currentSelection)
        {
            foreach (Transform child in transform)
            {
                    ChangeColour(child);
            }
        }
       
    }


    void OnMouseExit()
    {
        highlighted = false;

        if (GetComponent<Transform>().tag != currentSelection)
        {
            foreach (Transform child in transform)
            {
                ResetColour(child);
            }
        }
        
    }


    public void ChangeColour(Transform transform)
    {
        if (transform.GetComponent<Renderer>() == null)
        {
            return;
        }

        transform.GetComponent<Renderer>().material.color = highlightItemColour;

        //Item Shader Test Code - do not delete
        //transform.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        //transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", highlightItemColour);
        
        if (isSocket)
        {
            transform.GetComponent<Renderer>().material.color = highlightSocketColour;
            transform.GetComponent<Transform>().localScale = new Vector3(0.375f, 0.375f, 0.375f);
        }
    }


    public void ResetColour(Transform transform)
    {
        if (transform.GetComponent<Renderer>() == null)
        {
            return;
        }

        transform.GetComponent<Renderer>().material.color = startColour;
        
        if (isSocket)
        {
            transform.GetComponent<Transform>().localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
    }




    void OnGUI()
    {
        if (highlighted && !isSocket && !string.IsNullOrEmpty(objectName))
        {
            GUI.Box(new Rect(Event.current.mousePosition.x + 10, Event.current.mousePosition.y, 100, 25), objectName);


            //GUI.Box(new Rect(Event.current.mousePosition.x - 155, Event.current.mousePosition.y, 150, 25), new GUIContent(objectName, "this box has a tooltip"));
            //GUI.Label(new Rect(10, 40, 100, 40), GUI.tooltip);
        }

    }
}
