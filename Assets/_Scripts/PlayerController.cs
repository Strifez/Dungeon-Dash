using UnityEngine;
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

	public VelocityRange velocityRange = new VelocityRange (300f, 1000f);

	//Private INSTANCES
	private Rigidbody2D _rigidbody2D; 
	private Transform _transform;
	private Animator _animator; 

	private float _movingValue = 0;
	private bool _isFacingRight =true;
	private bool _isGrounded = true;

	// Use this for initialization
	void Start () {
		this._rigidbody2D = gameObject.GetComponent<Rigidbody2D> (); //referencing the rigidbody 2d and transform
		this._transform = gameObject.GetComponent<Transform> ();
		this._animator = gameObject.GetComponent<Animator> ();
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
				this._animator.SetInteger ("AnimState", 0);
			
			}
		
			//check if player is jumping 
			if ((Input.GetKey ("up") || Input.GetKey (KeyCode.W))) {
				if (this._isGrounded) {
					this._animator.SetInteger ("AnimState", 2);
					if (absVelY < this.velocityRange.vMax) {
						forceY = this.jump;
						this._isGrounded = false;
					}
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

	/*void OnCollisionEnter2D(Collision2D otherCollider){
		if (otherCollider.gameObject.CompareTag ("Coin")) {
		this._coinSound.Play ();
		}
	}*/

	//private methods
	private void _flip(){
	if (this._isFacingRight) {
			this._transform.localScale = new Vector3 (1f, 1f, 1f);
		
		} else {
			this._transform.localScale = new Vector3 (-1f, 1f, 1f);
		}
	}
}

