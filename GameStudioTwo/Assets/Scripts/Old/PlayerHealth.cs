using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public HPBar healthBar;
    public Text finish;
    public Button restartButton;
	public Image gameOverScreen;
    public Text flippedCounterText;

    private int flippedCounter;

    public AnimationCurve flippedCounterSize;
    public float flippedCounterSizeVar;

    private bool analyticsSent = false;
    private bool flipCoroutineStarted = false;
    private bool gameIsOver = false;

    public string controllerA = "SocketFront";

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
        if (transform.GetComponent<VehicleController>().player == 2)
        {
            controllerA += "P2";
        }

        flippedCounterSizeVar = 1.0f;
        healthBar.maxValue = maxHealth;
        health = maxHealth;
        calculateMass();
    }
	
	// Update is called once per frame
	void Update ()
    {
        flippedCounterSizeVar += Time.deltaTime;
        if (finish == null)
            return;

        calculateMass();
        if (health <= 0)
            gameIsOver = true;

        if (gameIsOver)
        {
            gameOver();
        }
        if (Vector3.Dot(transform.up, Vector3.up) < 0 && flipCoroutineStarted == false)
        {
            flipCoroutineStarted = true;
            StartCoroutine(checkFlipped());
        }
        else if(Vector3.Dot(transform.up,Vector3.up) >= 0)
        {
            flippedCounterText.text = "";
            flipCoroutineStarted = false;
            StopCoroutine(checkFlipped());
        }
        float temp = (flippedCounterSize.Evaluate(flippedCounterSizeVar) * 300);
        flippedCounterText.fontSize = (int)temp;

	}

    public void issueDamage(Collision collision)
    {
        //Determine damage multiplier
        float damageMultiplier = 1.0f;

        PlayerHealth enemy = collision.gameObject.GetComponent<PlayerHealth>();
        //float damage = collision.relativeVelocity.magnitude;
        float damage = collision.impulse.magnitude;
        float thisDamage = 0;

        //check if hit by weapon
        foreach (ContactPoint contact in collision.contacts)
        {
            weaponStats weapon = contact.otherCollider.gameObject.GetComponent<weaponStats>();
            weaponStats armor = contact.thisCollider.gameObject.GetComponent<weaponStats>();
            if (weapon != null)
            {           
                if(weapon.damageMultiplier > 1)
                    damageMultiplier = weapon.damageMultiplier;
            }
            if (armor != null)
            {
                if (armor.damageMultiplier < 1)
                    damageMultiplier *= armor.damageMultiplier;
            }

            thisDamage = damage * damageMultiplier * (enemy? enemy.mass : 1.0f);

            if (armor != null)
                armor.issueDamageAttachment(thisDamage);

        }

        Debug.Log("Damage Multiplier: " + damageMultiplier);
        Debug.Log(collision.gameObject.tag + " Mass: " + (enemy ? enemy.mass : 1.0f));

        this.issueDamage(thisDamage);

        Debug.Log(gameObject.name + " has " + health + " remaining");

        /* TODO: Re-enable this; Deinyon disabled analytics for now, because of compiler errors.
        Analytics.CustomEvent("Hit", new Dictionary<string, object> 
        {
            {"Was Hit", gameObject.name},
            {"Hit by", enemy.name},
            {"Hit for", thisDamage},
            {"Remaining HP", health}
        });*/
    }

    public void issueDamage(float damage)
    {
        Debug.Log(gameObject.tag + " hit for " + damage);
        health -= damage;
    }

    void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            issueDamage(collision);
        }

        if (collision.gameObject.CompareTag("Weapon"))
        {
            hazardDamage(collision);
        }
        
    }

    private void hazardDamage(Collision collision)
    {
        float damageMultiplier = 1.0f;
        float damage = collision.relativeVelocity.magnitude;
        float thisDamage = 0;

        //check if hit by weapon
        foreach (ContactPoint contact in collision.contacts)
        {
            weaponStats weapon = contact.otherCollider.gameObject.GetComponent<weaponStats>();
            weaponStats armor = contact.thisCollider.gameObject.GetComponent<weaponStats>();
            if (weapon != null)
            {
                if (weapon.damageMultiplier > 1)
                    damageMultiplier = weapon.damageMultiplier;
            }
            if (armor != null)
            {
                if (armor.damageMultiplier < 1)
                    damageMultiplier *= armor.damageMultiplier;
            }

            thisDamage = damage * damageMultiplier * weapon.mass;
            weapon.issueDamageAttachment(thisDamage);


            if (armor != null)
                armor.issueDamageAttachment(thisDamage);

        }

        this.issueDamage(thisDamage);

        Debug.Log(gameObject.name + " has " + health + " remaining");
    }

    private void calculateMass()
    {
        mass = 1;
        weaponStats[] allChildren = GetComponentsInChildren<weaponStats>();
        foreach (weaponStats child in allChildren)
        {
            mass += child.mass;
        }
        gameObject.GetComponent<Rigidbody>().mass = 50 * mass;
    }

    void gameOver()
    {
        string winner = "Bot";
        if (gameObject.CompareTag("Player"))
        {
            winner = "Bot";
            finish.text = "You Lose!";
        }

        else if (gameObject.CompareTag("Enemy"))
        {
            finish.text = "You win!";
            winner = "Player";
        }
		Time.timeScale = 0.0f;
		//Show Game Over Screen 
		//restartButton.gameObject.SetActive(true);
		finish.gameObject.SetActive (true);
		Color gameOverScrColor = new Color(0.3f,0.5f,1,1);
		gameOverScreen.color = gameOverScrColor;

        if (!analyticsSent)
        {
            /* TODO: Re-enable this; Deinyon disabled analytics for now, because of compiler errors.
            Analytics.CustomEvent("gameOver", new Dictionary<string, object> 
            {
                {"Winner", winner},
                {"Remaining HP", health},
            });
            */
            analyticsSent = true;
        }
        if (Application.loadedLevelName != "BattleScene03Multi")
        {
            if (Input.GetButtonDown(controllerA) || Input.GetKeyDown(KeyCode.Escape))
            {
                Application.LoadLevel("ItemStore");
            }
        }
        else
        {
            if (Input.GetButtonDown(controllerA) || Input.GetKeyDown(KeyCode.Escape))
            {
                Application.LoadLevel("MultiStore");
            }
        }
	
        
    }

	IEnumerator showGameOver() {
		yield return new WaitForSeconds(2);

	}

    IEnumerator checkFlipped()
    {
        flippedCounterSizeVar = 0;
        flippedCounterText.text = "5";
        yield return new WaitForSeconds(1);
        flippedCounterSizeVar = 0;
        flippedCounterText.text = "4";
        yield return new WaitForSeconds(1);
        flippedCounterSizeVar = 0;
        flippedCounterText.text = "3";
        yield return new WaitForSeconds(1);
        flippedCounterSizeVar = 0;
        flippedCounterText.text = "2";
        yield return new WaitForSeconds(1);
        flippedCounterSizeVar = 0;
        flippedCounterText.text = "1";
        yield return new WaitForSeconds(1);
        flippedCounterText.text = "";
        if (Vector3.Dot(transform.up, Vector3.up) < 0)
        {
            gameIsOver = true;
        }
        StopCoroutine("checkFlipped");
    }

}
