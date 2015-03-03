using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SinglePlayerGameManager : GameManager {
	
	AudioSource audioSrc;
	
	public GameObject bobPrefab;
	public GameObject bobItalyPrefab;
	public GameObject bobFrancePrefab;
	public GameObject bobCanadaPrefab;
	
	public GameObject garyPrefab;
	public GameObject garyYellowPrefab;
	public GameObject garyRedPrefab;
	public GameObject garyJapanPrefab;
	
	public GameObject garyProjectorPrefab;
	public GameObject bobProjectorPrefab;

	public AudioSource horn;
	public AudioSource countdown;

	public GameObject player1Spawn;
	
	public GameObject projector1Spawn;
	
	public GameObject barrier1;
	public GameObject barrier2;
	
	GameObject proj1;
	
	float bestScore;

	float gameTimeElapsed;
	
	public bool gameOver;
	public bool gamePlaying;
	public bool gameStarting;
	
	public float gameTime;
	public float startGameTime;
	
	public float preGameTimeMax = 3;
	float preGameTime;
	
	public Text timeText;
	public Text scoreTimeText;
	public Text finishText;
	public Text recordText;
	public Text record2Text;
	public Text bestScoreText;
	public GameObject replayText;
	public Text countDownText;

	Animator countDownAnim;
	
	void Awake() {
		GameManager.gm = this;
	}
	// Use this for initialization
	void Start () {
		countDownAnim = countDownText.GetComponent<Animator>();
		GameControl.control.StopMenuMusic();
		switch(GameControl.control.gameStyle) {
		case 0:
			startGameTime = 15f;
			break;
		case 1:
			startGameTime = 10f;
			break;
		case 2:
			startGameTime = 7f;
			break;
		}
		SpawnPlayers();
		SpawnProjectors();
		audioSrc = GetComponent<AudioSource>();
		Reset();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(gamePlaying) {
			ReduceGameTime();
			AddGameTimeElapsed();
			if(gameTime <= 0f) {
				gameTime = 0;
				GameOver();
			}
			
		}
		else if(gameStarting) {
			preGameTime -= Time.deltaTime;
			countDownText.text = preGameTime.ToString("f2");
			if(preGameTime <= 0f)
				StartGame();
		}
		else if(gameOver)
		{
			if(Input.GetKey(KeyCode.Space))
				Reset();
		}
	}
	
	void DisplayTime() {
		timeText.text = "Time: " + ((int)gameTime)/60 + ":" + (((int)gameTime)%60).ToString("d2");
	}
	
	void Reset() {
		p1.transform.position = player1Spawn.transform.position;
		barrier1.SetActive(true);
		barrier2.SetActive(true);
		preGameTime = preGameTimeMax;
		gameOver = false;
		gamePlaying = false;
		gameStarting = true;
		score1 = 0;
		score2 = 0;
		gameTime = startGameTime;
		DisplayTime();
		finishText.text = "";
		bestScoreText.text = bestScore.ToString("f1");
		recordText.enabled = false;
		record2Text.enabled = false;
		replayText.SetActive(false);
		countDownText.enabled = true;
		gameTimeElapsed = 0;
		scoreTimeText.text = gameTimeElapsed.ToString("f1");
		countdown.Play();
		countDownAnim.SetTrigger("Start");
	}
	
	void StartGame(){
		countDownText.enabled = false;
		barrier1.SetActive(false);
		barrier2.SetActive(false);
		gamePlaying = true;
		gameOver = false;
		gameStarting = false;
	}
	
	void GameOver() {
		horn.Play();
		gameOver = true;
		gamePlaying = false;
		gameStarting = false;
		if(gameTimeElapsed > bestScore)
			NewRecord();
		else
			Finished();
		replayText.SetActive(true);
	}
	
	public override void Player1Portal(){
		//Player is set as player2 for arrow key movement
	}
	
	public override void Player2Portal(){
		if(gamePlaying) {
			switch (GameControl.control.gameStyle) {
			case 0:
				gameTime += 7;
				break;
			case 1:
				gameTime += 5;
				break;
			case 2:
				gameTime += 2;
				break;
			}
		}
	}
	
	public override void Player1Catch(){
		//Do Nothing
	}
	
	public override void Player2Catch(){
		//Do Nothing
	}

	public void Finished() {
		finishText.text = gameTimeElapsed.ToString("f1");
	}

	public void NewRecord() {
		recordText.enabled = true;
		record2Text.enabled = true;
		record2Text.text = gameTimeElapsed.ToString("f1");
		recordText.text = "New Record!";
		bestScore = gameTimeElapsed;
		bestScoreText.text = bestScore.ToString("f1");
	}
	
	public void Player2Win() {
		//Do Nothing
	}
	
	void ReduceGameTime()
	{
		int oldTime = (int)gameTime;
		gameTime -= Time.deltaTime;
		int newTime = (int)gameTime;
		if(oldTime != newTime){
			audioSrc.Play();
			Debug.Log("Beep");
		}
		DisplayTime();
	}

	void AddGameTimeElapsed()
	{
		gameTimeElapsed += Time.deltaTime;
		scoreTimeText.text = gameTimeElapsed.ToString("f1");
	}
	
	void SpawnProjectors(){
		if(GameControl.control.characterNumP1 == 0)
			proj1 = (GameObject)GameObject.Instantiate(garyProjectorPrefab, projector1Spawn.transform.position, projector1Spawn.transform.rotation);
		else
			proj1 = (GameObject)GameObject.Instantiate(bobProjectorPrefab, projector1Spawn.transform.position, projector1Spawn.transform.rotation);
		proj1.GetComponent<ProjectShadowTowards>().obj = p1.transform;
	}
	
	void SpawnPlayers(){
		if(GameControl.control.characterNumP1 == 0) {
			switch (GameControl.control.styleNumP1) {
			case 0:
				p1 = (GameObject)GameObject.Instantiate(garyPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			case 1:
				p1 = (GameObject)GameObject.Instantiate(garyYellowPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			case 2:
				p1 = (GameObject)GameObject.Instantiate(garyRedPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			case 3:
				p1 = (GameObject)GameObject.Instantiate(garyJapanPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			}
			p1.GetComponent<CharacterController2D>().SetPlayer(2);	//Set to arrow keys
		}
		else{
			switch (GameControl.control.styleNumP1) {
			case 0:
				p1 = (GameObject)GameObject.Instantiate(bobPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			case 1:
				p1 = (GameObject)GameObject.Instantiate(bobFrancePrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			case 2:
				p1 = (GameObject)GameObject.Instantiate(bobCanadaPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			case 3:
				p1 = (GameObject)GameObject.Instantiate(bobItalyPrefab, player1Spawn.transform.position, player1Spawn.transform.rotation);
				break;
			}
			p1.GetComponent<BallMove>().SetPlayer(2);
		}
	}
}
