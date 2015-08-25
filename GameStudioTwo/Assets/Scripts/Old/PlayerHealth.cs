using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    //public Slider healthSlider;
    public HPBar healthBar;
    public Text finish;
    public Button restartButton;

    public float
        maxHealth = 50.0f,
        mass = 1.0f,
        damageFactor = 1.0f;


    // Never set this directly! Anywhere! Yes, this means you!
    private float _health;
    public float health
    {
        get { return _health; }
        set
        {
            _health = value<0? 0 : value;
            healthBar.value = _health;
        }
    }

    void Start()
    {
        healthBar.maxValue = maxHealth;
        health = maxHealth;
        calculateMass();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (finish == null)
            return;

        if (health <= 0)
            gameOver();
        if (Vector3.Dot(transform.up,Vector3.up) < 0)
        {
            StartCoroutine(checkFlipped());
        }
	}

    public void issueDamage(Collision collision)
    {
        // Rob's:
        //float damage = Random.Range(collision.relativeVelocity.magnitude - 2, collision.relativeVelocity.magnitude + 2);

        //Determine damage multiplier
        float damageMultiplier = 1.0f;
        foreach (ContactPoint contact in collision.contacts)
        {
            weaponStats weapon = contact.otherCollider.gameObject.GetComponent<weaponStats>();
            if (weapon != null)
            {
                if (weapon.damageMultiplier > damageMultiplier)
                {
                    damageMultiplier = weapon.damageMultiplier;
                }
            }
        }

        PlayerHealth enemy = collision.gameObject.GetComponent<PlayerHealth>();

        float damage = collision.relativeVelocity.magnitude;


        //Deinyon's code to make damage only happen on T-Bones
        //float damageAngleMag = Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, -collision.collider.transform.right));
        //float otherDamage = damage * (1.0f - damageAngleMag);


        float thisDamage = damage * damageMultiplier * enemy.mass;
        Debug.Log("Damage Multiplier: " + damageMultiplier);
        Debug.Log(collision.gameObject.tag + " Mass: " + enemy.mass);

        this.issueDamage(thisDamage);
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

    private void calculateMass()
    {
        weaponStats[] allChildren = GetComponentsInChildren<weaponStats>();
        foreach (weaponStats child in allChildren)
        {
            mass += child.mass;
        }
    }

    void gameOver()
    {
        if (gameObject.CompareTag("Player"))
            finish.text = "You Lose";

        else if (gameObject.CompareTag("Enemy"))
            finish.text = "You win";


        Time.timeScale = 0.0f;

        restartButton.gameObject.SetActive(true);
        if (Input.GetButtonDown("Boost"))
        {
            Application.LoadLevel(1);
        }
    }

    IEnumerator checkFlipped()
    {
        yield return new WaitForSeconds(5);
        if (Vector3.Dot(transform.up, Vector3.up) < 0)
            gameOver();
        else
            StopCoroutine("checkFlipped");
    }
}
