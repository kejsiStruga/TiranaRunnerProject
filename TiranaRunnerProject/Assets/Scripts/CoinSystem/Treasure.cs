using UnityEngine;
using System.Collections;
using Assets.CoinsScripts;

public class Treasure : MonoBehaviour {

	public GameObject explosionPrefab;

	void OnTriggerEnter (Collider other) {

		if (other.gameObject.tag == "Player") {

            // explode if specifie
			if (explosionPrefab != null) {
                /*
                 * Instantiate Explosion
                 */
				Instantiate (explosionPrefab, transform.position, Quaternion.identity);
			}
		}
	}
}
