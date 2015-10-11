using UnityEngine;
using System.Collections;

public class PlasmaEffect : MonoBehaviour {

    public float speed;
    float lightSpeed = 100f;
    public bool hasLight;


    void FixedUpdate() {
        transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), speed * Time.deltaTime);
    }


    void Update()
    {
        if (hasLight)
        {
            float ping = Mathf.PingPong(200 * Time.time, 255.0f);
            //Debug.Log(ping);

            Color newColour = new Color(125 / 255f, ping / 255f, 125 / 255f);

            //transform.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

            transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", newColour);
            transform.GetComponent<Renderer>().material.color = newColour;

            Light[] lights = this.GetComponentsInChildren<Light>();

            foreach (Light child in lights)
            {
                child.color = newColour;
            }

        }
    }


}
