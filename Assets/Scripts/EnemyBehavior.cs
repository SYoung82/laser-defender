using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
	private float health = 150.0f;

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile) {
			missile.Hit();
			health -= missile.GetDamage();
			if(health <= 0) {
				Destroy(gameObject);
			}
			Debug.Log("Hit by a projectile");
		}
	}
}
