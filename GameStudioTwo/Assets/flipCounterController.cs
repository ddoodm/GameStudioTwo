using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class flipCounterController : MonoBehaviour
{
    public Text player;
    public Text Enemy;

    public void TriggerFlipTimeout(GameObject sender, Action callback)
    {
        Rigidbody body = sender.transform.root.GetComponent<Rigidbody>();
        if (body == null)
            return;

        StartCoroutine(UnflipBody(body, callback));
    }

    private IEnumerator UnflipBody(Rigidbody body, Action callback)
    {
        float startTime = Time.time;

        while(BodyUneven(body))
        {
            body.GetComponent<PlayerHealth>().enabled = false;

            body.AddForce(Vector3.up * (5.0f - body.position.y) * 0.1f, ForceMode.VelocityChange);

            /*
            body.transform.rotation = (Quaternion.Euler(body.transform.localEulerAngles
                - (body.transform.localEulerAngles) * 0.5f * Time.deltaTime));*/

            body.transform.rotation = Quaternion.Lerp(
                body.transform.rotation,
                Quaternion.identity,
                (Time.time - startTime) * 0.005f
                );

            yield return new WaitForFixedUpdate();
        }

        // Finished, call the callback
        callback();
        body.GetComponent<PlayerHealth>().enabled = true;
        body.isKinematic = false;
    }

    private bool BodyUneven(Rigidbody body)
    {
        return Vector3.Dot(body.transform.up, Vector3.up) < 0.98f;
    }
	
	// Update is called once per frame
	void Update ()
    {

        /* Y U DO DIS ROBBBBB????
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
        */
	}
}
