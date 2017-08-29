using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	private float width = 8.0f;
	private float height = 4.0f;
	private float xMin;
	private float xMax;
	private float speed = 2.0f;

	void Start () {
		foreach(Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity);
			enemy.transform.parent = child;
		}

		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector2 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector2 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xMin = leftmost.x + width/2;
		xMax = rightmost.x - width/2;
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
	}
	
	void Update () {
		// If formation has reached either edge, reverse speed
		if(transform.position.x == xMin || transform.position.x == xMax) {
			speed *= -1;
		}

		transform.position += Vector3.left * speed * Time.deltaTime;
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
