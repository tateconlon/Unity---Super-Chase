using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChaseGameManager : GameManager{

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
	
	public GameObject chaserSpawn;
	public GameObject runnerSpawn;
	
	public GameObject projector1Spawn;
	public GameObject projector2Spawn;
	
	public GameObject barrier1;
	public GameObject barrier2;

	public AudioSource horn;
	public AudioSource countdown;

	Transform player1Spawn;
	Transform player2Spawn;

	GameObject proj1;
	GameObject proj2;
	
	public bool gameOver;
	public bool gamePlaying;
	public bool gameStarting;
	
	public float gameTime;
	
	public float preGameTimeMax = 3;
	float preGameTime;
	
	public Text timeText;
	public Text winText;
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
		score1 = 0;
		score2 = 0;
		SpawnPlayers();
		SpawnProjectors();
		audioSrc = GetComponent<AudioSource>();
		Reset();
	}
	
	// Update is called once per frame
	void Update () {

		if(gamePlaying) {
			ReduceGameTime();
			if(gameTime <= 0f) {
				gameTime = 0;
				TimeUp();
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
		countdown.Play();
		p1.transform.position = player1Spawn.position;
		p2.transform.position = player2Spawn.position;
		barrier1.SetActive(true);
		barrier2.SetActive(true);
		preGameTime = preGameTimeMax;
		gameOver = false;
		gamePlaying = false;
		gameStarting = true;
		gameTime = GameControl.control.maxTime;
		DisplayTime();
		winText.text = "";
		replayText.SetActive(false);
		countDownText.enabled = true;
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

	void TimeUp() {
		if(GameControl.control.gameStyle == 0)
			Player2Win();
		else
			Player1Win();
		GameOver();
	}

	void GameOver() {
		horn.Play();
		gameOver = true;
		gamePlaying = false;
		gameStarting = false;
		replayText.SetActive(true);
	}
	
	public override void Player1Portal(){
		//Do Nothing
	}
	
	public override void Player2Portal(){
		//Do Nothing
	}
	
	public override void Player1Catch(){
		if(gamePlaying) {
			Player1Win();
		}
	}
	
	public override void Player2Catch(){
		if(gamePlaying)
			Player2Win();
	}
	
	public void Player1Win() {
		GameOver();
		score1++;
		winText.color = new Color(1f ,0f, 0f, 1f);	//Todo: colour selecting 	winText.color = new Color(141f,6f,76f,255f)/255;
		winText.text = "Player 1 Wins!";
	}
	
	public void Player2Win() {
		GameOver();
		score2++;
		winText.color = new Color(9f ,0f, 255f, 255f)/255f;	//Todo: colour selecting 	winText.color = new Color(141f,6f,76f,255f)/255;
		winText.text = "Player 2 Wins!";
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
	
	void SpawnProjectors(){
		if(GameControl.control.characterNumP1 == 0)
			proj1 = (GameObject)GameObject.Instantiate(garyProjectorPrefab, projector1Spawn.transform.position, projector1Spawn.transform.rotation);
		else
			proj1 = (GameObject)GameObject.Instantiate(bobProjectorPrefab, projector1Spawn.transform.position, projector1Spawn.transform.rotation);
		proj1.GetComponent<ProjectShadowTowards>().obj = p1.transform;
		
		if(GameControl.control.characterNumP2 == 0)
			proj2 = (GameObject)GameObject.Instantiate(garyProjectorPrefab, projector2Spawn.transform.position, projector2Spawn.transform.rotation);
		else
			proj2 = (GameObject)GameObject.Instantiate(bobProjectorPrefab, projector2Spawn.transform.position, projector2Spawn.transform.rotation);
		proj2.GetComponent<ProjectShadowTowards>().obj = p2.transform;
		
	}
	
	void SpawnPlayers(){
		if(GameControl.control.gameStyle == 0) {
			player1Spawn = chaserSpawn.transform;
			player2Spawn = runnerSpawn.transform;
		}
		else {
			player1Spawn = runnerSpawn.transform;
			player2Spawn = chaserSpawn.transform;
		}
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
			CharacterController2D cc = p1.GetComponent<CharacterController2D>();
			cc.SetPlayer(1);
			if(GameControl.control.gameStyle == 0)
				cc.isChaser = true;
			else
				cc.isChaser = false;
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
			BallMove bm = p1.GetComponent<BallMove>();
			bm.SetPlayer(1);
			if(GameControl.control.gameStyle == 0)
				bm.isChaser = true;
			else
				bm.isChaser = false;
		}
		
		if(GameControl.control.characterNumP2 == 0) {
			switch (GameControl.control.styleNumP2) {
			case 0:
				p2 = (GameObject)GameObject.Instantiate(garyPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			case 1:
				p2 = (GameObject)GameObject.Instantiate(garyYellowPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			case 2:
				p2 = (GameObject)GameObject.Instantiate(garyRedPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			case 3:
				p2 = (GameObject)GameObject.Instantiate(garyJapanPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			}
			CharacterController2D cc = p2.GetComponent<CharacterController2D>();
			cc.SetPlayer(2);
			if(GameControl.control.gameStyle == 0)
				cc.isChaser = false;
			else
				cc.isChaser = true;
		}
		else{
			switch (GameControl.control.styleNumP2) {
			case 0:
				p2 = (GameObject)GameObject.Instantiate(bobPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			case 1:
				p2 = (GameObject)GameObject.Instantiate(bobFrancePrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			case 2:
				p2 = (GameObject)GameObject.Instantiate(bobCanadaPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			case 3:
				p2 = (GameObject)GameObject.Instantiate(bobItalyPrefab, player2Spawn.transform.position, player2Spawn.transform.rotation);
				break;
			}
			BallMove bm = p2.GetComponent<BallMove>();
			bm.SetPlayer(2);
			if(GameControl.control.gameStyle == 0)
				bm.isChaser = false;
			else
				bm.isChaser = true;
		}
	}
}
