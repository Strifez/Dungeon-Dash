﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class VelocityRange {
	public float vMin, vMax;
	
	public VelocityRange(float vMin, float vMax){
		this.vMin=vMin;
		this.vMax = vMax;
	}
}

public class PlayerController : MonoBehaviour {

	//Public INSTANCES
	public float speed = 50f;
	public float jump = 500f;
	public Transform sightStart;	//LINECASTING start
	public Transform sightEnd;		//LINECASTING end

	public VelocityRange velocityRange = new VelocityRange (300f, 1000f);

	public GameObject playerArrow;
	public GameObject arrowPosition;
	public float fireRate;

	//Private INSTANCES
	private Rigidbody2D _rigidbody2D; 
	private Transform _transform;
	private Animator _animator; 

	private float _movingValue = 0;
	private bool _isFacingRight =true;
	private bool _isGrounded = true;
	private bool _isGroundBelow = false;	//LINECASTING 

	private float nextFire;

	private AudioSource[] _audioSource; //array of many sounds
	private AudioSource _coinSound; // one sound
	private AudioSource _bkgSound;

	// Use this for initialization
	void Start () {
		this._rigidbody2D = gameObject.GetComponent<Rigidbody2D> (); //referencing the rigidbody 2d and transform
		this._transform = gameObject.GetComponent<Transform> ();
		this._animator = gameObject.GetComponent<Animator> ();

		this._audioSource = gameObject.GetComponents<AudioSource> ();
		this._bkgSound = this._audioSource [0]; //assigning this to array element 0
		this._coinSound = this._audioSource [1];
	}

	void Update (){
		if (Input.GetKeyDown ("space") && Time.time > nextFire) { //the fire button will be the spacebar
			
			nextFire = Time.time + fireRate;
			GameObject arrow= (GameObject) Instantiate (playerArrow); //Instantiate the bullet
			arrow.transform.position = arrowPosition.transform.position; //set initial bullet position
		}
	}
	
	// Using Physics motion
	void FixedUpdate () {
		float forceX = 0f;
		float forceY = 0f;
		
		float absVelX = Mathf.Abs (this._rigidbody2D.velocity.x); 
		float absVelY = Mathf.Abs (this._rigidbody2D.velocity.y);
		
		this._movingValue = Input.GetAxis ("Horizontal"); // gives moving variable a value of -1 or 1
		
		//check player is moving
		if (this._movingValue != 0) {
			//check if player is grounded
			if (this._isGrounded) {
				//change animation to 1, 1 was set to Player Run Animation
				this._animator.SetInteger ("AnimState", 1);
				if (this._movingValue > 0) {
					//move right
					if (absVelX < this.velocityRange.vMax) {
						forceX = this.speed;
						this._isFacingRight = true;
						this._flip ();
					}
				} else if (this._movingValue < 0) {
					// move left
					if (absVelX < this.velocityRange.vMax) {
						forceX = -this.speed;
						this._isFacingRight = false;
						this._flip ();
					}
				}
			}
			} else {
				// 0 was set to Player Idle Animation
				this._animator.SetInteger ("AnimState", 0);
			
			}
		
			//check if player is jumping 
			if ((Input.GetKey ("up") || Input.GetKey (KeyCode.W))) { 
				if (this._isGrounded) {
					// 2 was set to Player Jump Animation
					this._animator.SetInteger ("AnimState", 2);
					//line cast added so the player does not shoot up into the air when its touching a wall.
					this._isGroundBelow = Physics2D.Linecast(this.sightStart.position, this.sightEnd.position, 1 << LayerMask.NameToLayer ("Ground")); 
					Debug.DrawLine(this.sightStart.position,this.sightEnd.position); //draws a line in debug mode to see if linecasting works

					//if there is ground below then allow the jump and make the player not grounded anymore.
					if(_isGroundBelow == true){ 
					if (absVelY < this.velocityRange.vMax) {
						forceY = this.jump;
						this._isGrounded = false;
					}
				}
					
				}
			}

			if (Input.GetKey ("space")){
			if (this._isGrounded){
				this._animator.SetInteger ("AnimState", 3);	
				}
		}
			//add force to push the player
			this._rigidbody2D.AddForce (new Vector2 (forceX, forceY));
			}


	void OnCollisionStay2D(Collision2D otherCollider){				// check if grounded CollisionStay
	if (otherCollider.gameObject.CompareTag ("Ground")) {
			this._isGrounded = true;
		}
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider){
		if (otherCollider.gameObject.CompareTag ("Coin")) {
		this._coinSound.Play ();
		}
	}
	
	//private methods
	//flips the character when you face the other way.
	private void _flip(){
	if (this._isFacingRight) {
			this._transform.localScale = new Vector3 (1f, 1f, 1f);
		
		} else {
			this._transform.localScale = new Vector3 (-1f, 1f, 1f);
		}
	}
}




