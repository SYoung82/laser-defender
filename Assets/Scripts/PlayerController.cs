using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject laserPrefab;
	private float health = 300f;
	private float laserSpeed = 5.0f;
	private float firingRate = 0.2f;
	private float speed = 5.0f;
	private float padding = 0.5f;
	private float xMin;
	private float xMax;

	void Start() {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xMin = leftmost.x + padding;
		xMax = rightmost.x - padding;
	}

	void Fire() {
		GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.0000001f, firingRate);
		}

		if(Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}

		if(Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}


		// Restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile) {
			missile.Hit();
			health -= missile.GetDamage();
			if(health <= 0) {
				Destroy(gameObject);
			}
		}
	}
}
