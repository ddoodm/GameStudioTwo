using UnityEngine;
using System.Collections;

public class Billboarder : MonoBehaviour
{
    public Camera camera;

    void Start ()
    {
        if (camera == null)
            camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

	void Update ()
    {
        this.transform.rotation =
            Quaternion.LookRotation(this.transform.position - camera.transform.position, Vector3.up);
    }
}
