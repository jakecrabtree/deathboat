using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;
	
	public float totalTime = 119f; //2 minutes
	public float timeRemaining;

	[SerializeField]
	int[] scoreThresholds;

	[SerializeField]
	string[] scoreLevelNames;

	[SerializeField]
	string[] scoreLevelDescriptions;

	[SerializeField]
	string[] scoreLevelFlavorTexts;

	[SerializeField]
	int startingScore = -25;

	[SerializeField]
	int lowerScoreBound;

	[SerializeField]
	int upperScoreBound;

	int currentScore;
	int scoreOffset = 0;

	bool gameIsOver = false;
	public SimpleHealthBar healthBar;

	public Text scoreText;
	public Text scoreThresholdText;

	public Text timerText;

	[SerializeField]
	private GameObject endGameBox;

	
	private SoundManager soundMng;

	void Awake()
	{
		//Check if instance already exists
		if (instance == null){
			instance = this;
		}
		else if (instance != this){
			Destroy(gameObject);   
		} 
		InitGame();
	}
	

	void InitGame(){
		currentScore = startingScore;
		timeRemaining = totalTime;
		scoreOffset = -1 * lowerScoreBound;
		UpdateScoreBar();
		UpdateTimerUI();
		endGameBox.SetActive(false);
		soundMng = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}

	void GameOver(){
		Debug.Log("game over");
		Time.timeScale = 0.0f;
		endGameBox.SetActive(true);
		int finalLevel = CurrentLevel(currentScore);
		endGameBox.transform.GetChild(3).GetComponent<Text>().text = scoreLevelFlavorTexts[finalLevel];
		endGameBox.transform.GetChild(4).GetComponent<Text>().text = scoreLevelDescriptions[finalLevel];
		gameIsOver = true;
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining = Mathf.Max(timeRemaining - Time.deltaTime, 0);
		UpdateTimerUI();
		if (gameIsOver){
			if (Input.GetKeyDown(KeyCode.Return)){
				Scene scene = SceneManager.GetActiveScene(); 
				SceneManager.LoadScene(scene.name);
				Time.timeScale = 1.0f;
			}else if (Input.GetKeyDown(KeyCode.Escape)){
				Application.Quit();
			}
		}
	}

	void UpdateTimerUI(){
		timerText.text = timeRemaining.ToString("F0");
		if (timeRemaining <= 0){
			GameOver();
		}
	}

	public void AddScore(int scoreValue){
		currentScore = Math.Min(scoreValue+currentScore, upperScoreBound);
		if(soundMng != null){
			soundMng.playKaChing();
		}
		UpdateScoreBar();
	}

	public void SubtractScore(int scoreValue){
		currentScore = Math.Max(currentScore-scoreValue, lowerScoreBound);
		UpdateScoreBar();
	}

	void UpdateScoreBar(){
		healthBar.UpdateBar(currentScore + scoreOffset, upperScoreBound + scoreOffset);
		scoreText.text = currentScore.ToString();
		scoreThresholdText.text = scoreLevelNames[CurrentLevel(currentScore)];
	}

	int CurrentLevel(int score){
		for (int i = 0; i < scoreThresholds.Length; ++i){
			if (score < scoreThresholds[i]){
				return i;
			}
		}
		return scoreLevelNames.Length-1;
	}
}
