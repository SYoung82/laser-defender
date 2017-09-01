using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	private int score;
	private Text scoreText;
	
	void Start() {
		scoreText = GetComponent<Text>();
		scoreText.text = score.ToString();
	}
	public void Score(int points) {
		score += points;
		scoreText.text = score.ToString();
	}

	void Reset() {
		score = 0;
		scoreText.text = score.ToString();
	}
}
