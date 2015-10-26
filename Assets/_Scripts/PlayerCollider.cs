using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour {

	public Text killLabel;
	public Text coinLabel;
	public Text lifeLabel;
	public int killValue = 0;
	public int coinValue = 0;
	public int lifeValue = 3;
	public Text gameOverLabel;
	public Text totalCoinLabel;
	public Text totalKillLabel;
	public int minusDmg= 1;
	public int coin =1;

	public SpriteRenderer spriteRenderer;
	public Rigidbody2D rigidbody2D;


	//private AudioSource[] _audioSources; // an array of AudioSources
	//private AudioSource _backgroundMusic, _splat;

	// Use this for initialization
	void Start () {
		this.gameOverLabel.enabled = false;			// this is for the GUI text making sure the gameover text is not displayed until its time
		this.totalCoinLabel.enabled = false;
		this.totalKillLabel.enabled = false;
		//this._audioSources = this.GetComponents<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D otherGameObject) { //Pick Up Item trigger
		if (otherGameObject.tag == "Coin") {
			CoinCheck (coin); //+ 100 points for picking up 
		}
		
		if (otherGameObject.tag == "Enemy") { 	//Enemy Trigger
			LifeCheck (minusDmg); // minus 1 life
		}

	}

	public void ScoreCheck(int newScoreCheck) // This checks and updates the score
	{
		killValue += newScoreCheck;
		DisplayKillScore ();
	}

	public void DisplayKillScore (){
		killLabel.text = "Kill: " + killValue; // This displays the score

	}

	public void CoinCheck(int newScoreCheck) // This checks and updates the score
	{
		coinValue += newScoreCheck;
		DisplayCoinScore ();
	}
	
	public void DisplayCoinScore (){
		coinLabel.text = "Coin: " + coinValue; // This displays the score
	}


	public void LifeCheck (int newLifeCheck)//This checks and updates the life
	{
		lifeValue -= newLifeCheck;
		DisplayLife ();
	}

	public void DisplayLife() // This displays the Life
	{
		lifeLabel.text = "Life: " + lifeValue;
		if (this.lifeValue <= 0) {
			this._EndGame ();
		}
	}

	public void _EndGame(){
		Destroy (this.spriteRenderer);
		Destroy (this.rigidbody2D);
		this.coinLabel.enabled = false;
		this.lifeLabel.enabled = false;
		this.killLabel.enabled = false;
		this.gameOverLabel.enabled = true;
		this.totalCoinLabel.enabled = true;
		this.totalKillLabel.enabled = true;
		this.totalCoinLabel.text = "Total Gold: " + this.coinValue;
		this.totalKillLabel.text = "Total Kills: " + this.killValue;
	}

}
