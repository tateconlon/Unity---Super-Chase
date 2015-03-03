using UnityEngine;
using System.Collections;

public abstract class GameManager : MonoBehaviour {

	public static GameManager gm;

	public GameObject p1;
	public GameObject p2;

	public int score1 = 0;
	public int score2 = 0;

	abstract public void Player1Portal();
	abstract public void Player2Portal();

	abstract public void Player1Catch();
	abstract public void Player2Catch();


}
