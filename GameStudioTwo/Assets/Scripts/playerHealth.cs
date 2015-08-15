using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerHealth : MonoBehaviour {

    public Slider healthSlider;
    public Text finish;

    public float
        maxHealth = 50.0f,
        damageFactor = 1.0f;

    // Never set this directly! Anywhere! Yes, this means you!
    private float _health;

    public float health
    {
        get { return _health; }
        set { _health = value; healthSlider.value = value; }
    }

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        health = maxHealth;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (health == 0)
        {
            if (gameObject.CompareTag("Player"))
                finish.text = "You Lose";
            else if (gameObject.CompareTag("Enemy"))
                finish.text = "You win";
        }
	}

    public void issueDamage(Collision collision)
    {
        // Rob's:
        //float damage = Random.Range(collision.relativeVelocity.magnitude - 2, collision.relativeVelocity.magnitude + 2);

        // Get the PlayerHealth of the collider
        playerHealth otherPlayerHealth = collision.gameObject.GetComponent<playerHealth>();

        float damage = collision.relativeVelocity.magnitude;

        float damageAngleMag = Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, -collision.collider.transform.right));
        float thisDamage = damage * (1.0f - damageAngleMag);
        float otherDamage = damage * damageAngleMag;

        this.issueDamage(thisDamage);
        otherPlayerHealth.issueDamage(otherDamage);
    }

    public void issueDamage(float damage)
    {
        Debug.Log(gameObject.tag + " hit for " + damage);
        health -= damage;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
            issueDamage(collision);
    }
}
