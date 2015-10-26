using UnityEngine;
using System.Collections;

public class LightningCreator : MonoBehaviour {

    public Lightning lightningPrefab;
    public bool electric;
    
    IEnumerator Start() {
	
        while(electric)
        {
            Instantiate(lightningPrefab, this.transform.position, Quaternion.identity);
            Instantiate(lightningPrefab, this.transform.position, Quaternion.identity);

            yield return null;
        }


	}
}
