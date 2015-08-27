using UnityEngine;
using System.Collections;

public class weaponStats : MonoBehaviour {

    public float mass, damageMultiplier, hp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        checkDestroy();
	
	}

    void checkDestroy()
    {
        if (hp <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public void issueDamageAttachment(float damage)
    {
        float attachmentMultiplier = Mathf.Abs(1 - damageMultiplier);
        if (damageMultiplier < 1)
            attachmentMultiplier += 1;
        

        Debug.Log("Damage before multiplier: " + damage);
        Debug.Log("Damage multiplier: " + attachmentMultiplier);
        Debug.Log(gameObject.name + " hit for " + damage * attachmentMultiplier);
        hp -= damage * attachmentMultiplier;
        Debug.Log("Remaining hp " + hp);
    }
}
