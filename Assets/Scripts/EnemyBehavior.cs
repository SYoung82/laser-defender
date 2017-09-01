using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public GameObject projectile;
	public AudioClip deathSound;
	private ScoreKeeper scoreKeeper;
	private float health = 150.0f;
	private float projectileSpeed = 5.0f;
	private float shotsPerSecond = 0.5f;
	private int scoreValue = 100;

	void Start() {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void Update() {
		float probability = shotsPerSecond * Time.deltaTime;
		if(Random.value < probability) {
			Fire();
		}
	}

	void Fire() {
		Vector3 startPosition = transform.position + new Vector3(0f, -1f, 0f);
		GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity);
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileSpeed);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile) {
			missile.Hit();
			health -= missile.GetDamage();
			if(health <= 0) {
				Die();
			}
		}
	}

	void Die() {
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		scoreKeeper.Score(scoreValue);
		Destroy(gameObject);
	}
}
