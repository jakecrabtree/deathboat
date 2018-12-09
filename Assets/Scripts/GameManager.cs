using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;
	
	public float totalTime = 119f; //2 minutes
	public float timeRemaining;

	[SerializeField]
	int[] scoreThresholds;

	[SerializeField]
	string[] scoreLevelNames;

	[SerializeField]
	int startingScore = -25;

	[SerializeField]
	int lowerScoreBound;

	[SerializeField]
	int upperScoreBound;

	int currentScore;
	int scoreOffset = 0;
	public SimpleHealthBar healthBar;
	public Text scoreText;
	public Text scoreThresholdText;

	public Text timerText;
	

	void Awake()
	{
		//Check if instance already exists
		if (instance == null){
			instance = this;
		}
		else if (instance != this){
			Destroy(gameObject);   
		} 
		DontDestroyOnLoad(gameObject);
		InitGame();
	}

	void InitGame(){
		currentScore = startingScore;
		timeRemaining = totalTime;
		scoreOffset = -1 * lowerScoreBound;
		UpdateScoreBar();
		UpdateTimerUI();
	}

	void GameOver(){
		Debug.Log("game over");
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining = Mathf.Max(timeRemaining - Time.deltaTime, 0);
		UpdateTimerUI();
	}

	void UpdateTimerUI(){
		timerText.text = timeRemaining.ToString("F0");
		if (timeRemaining <= 0){
			GameOver();
		}
	}

	public void AddScore(int scoreValue){
		currentScore = Math.Min(scoreValue+currentScore, upperScoreBound);
		UpdateScoreBar();
	}

	public void SubtractScore(int scoreValue){
		currentScore = Math.Max(currentScore-scoreValue, lowerScoreBound);
		UpdateScoreBar();
	}

	void UpdateScoreBar(){
		healthBar.UpdateBar(currentScore + scoreOffset, upperScoreBound + scoreOffset);
		scoreText.text = currentScore.ToString();
		scoreThresholdText.text = CurrentLevel(currentScore);
	}

	string CurrentLevel(int score){
		for (int i = 0; i < scoreThresholds.Length; ++i){
			if (score < scoreThresholds[i]){
				return scoreLevelNames[i];
			}
		}
		return scoreLevelNames[scoreLevelNames.Length-1];
	}
}
