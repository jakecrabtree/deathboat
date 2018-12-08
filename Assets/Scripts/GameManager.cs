using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;
	
	public float totalTime = 120f; //2 minutes
	public float timeRemaining;

	[SerializeField]
	int lowerScoreBound = -100;

	[SerializeField]
	int startingScore = 0;

	[SerializeField]
	int upperScoreBound = 100;

	int currentScore;
	int scoreOffset = 0;
	public SimpleHealthBar healthBar;
	

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
	}

	void GameOver(){
		Debug.Log("game over");
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining = Mathf.Max(timeRemaining - Time.deltaTime, 0);
		if (timeRemaining <= 0){
			GameOver();
		}
	}

	public void AddScore(int scoreValue){
		currentScore += scoreValue;
		healthBar.UpdateBar(currentScore + scoreOffset, upperScoreBound + scoreOffset);
	}

	public void SubtractScore(int scoreValue){
		currentScore -= scoreValue;
		healthBar.UpdateBar(currentScore + scoreOffset, upperScoreBound + scoreOffset);
	}
}
