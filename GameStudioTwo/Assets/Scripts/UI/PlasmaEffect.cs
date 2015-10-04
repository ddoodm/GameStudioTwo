using UnityEngine;
using System.Collections;

public class PlasmaEffect : MonoBehaviour {

    public float speed;
    public bool hasLight;


	void FixedUpdate () {
       


		transform.Rotate (new Vector3(0.0f, 0.0f, 1.0f), speed * Time.deltaTime);

        /*
        float newpos = Mathf.PingPong(Time.deltaTime, 255);

        transform.GetComponent<Renderer>().material.color = Color.green;
        */



    }









}
