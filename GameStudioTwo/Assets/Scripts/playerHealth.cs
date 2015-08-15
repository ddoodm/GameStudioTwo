using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerHealth : MonoBehaviour {

    public Slider health;
    public Text finish;
	
	// Update is called once per frame
	void Update ()
    {
        if (health.value == 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                finish.text = "You Lose";
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                finish.text = "You win";
            }
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            float damage = Random.Range(collision.relativeVelocity.magnitude - 2, collision.relativeVelocity.magnitude + 2);
            Debug.Log(gameObject.tag + " hit for " + damage);
            health.value -= damage;
        }
    }
}
