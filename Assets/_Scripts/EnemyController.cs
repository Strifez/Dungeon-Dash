using UnityEngine;
using System.Collections;

//Jason Huang 300818592
//Source: Professor Tom's Mail Pilot
//Last Modified: Oct,26,2015
//Description: Script that makes the Enemy move and patrol and collision detection

public class EnemyController : MonoBehaviour {
	//PUBLIC INSTANCES
	public float speed = 100f;
	public Transform sightStart;	//LINECASTING - for example enemys flip when they reach the end of a platform and
	public Transform sightEnd;		// not just fall off. 
	public int minusDmg;

	//PRIVATE INSTANCES 
	private Rigidbody2D _rigidbody2D; 
	private Transform _transform;
	private Animator _animator;
	
	private bool _isGrounded = false;
	private bool _isGroundAhead = false;

	private PlayerCollider playerCollider; //again we need to reference a method
	
	// Use this for initialization
	void Start () {
		this._rigidbody2D = gameObject.GetComponent<Rigidbody2D> (); //referencing the rigidbody 2d and transform
		this._transform = gameObject.GetComponent<Transform> ();
		this._animator = gameObject.GetComponent<Animator> ();

		GameObject playerColliderObject = GameObject.FindWithTag("Player"); // allows us to pull Life Check Method from the player collider script
		if (playerColliderObject != null)
		{
			playerCollider = playerColliderObject.GetComponent<PlayerCollider>();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// check to see if enemy is grounded
		if (this._isGrounded) {
			this._animator.SetInteger("AnimState", 1);
			
			// you want the enemy to start walking and not just stand there (localScale required for the flip)
			this._rigidbody2D.velocity= new Vector2 (this._transform.localScale.x, 0) * -this.speed; //- because my enemy is facing left
			
			//LINECASTING - Check to see if there is ground ahead, layermask is (1=true) read it right to left if layermask is solid then it is true
			this._isGroundAhead = Physics2D.Linecast(this.sightStart.position, this.sightEnd.position, 1 << LayerMask.NameToLayer ("Ground"));
			Debug.DrawLine(this.sightStart.position,this.sightEnd.position); //draws a line in debug mode to see if linecasting works
			
			if(_isGroundAhead == false){ // makes the enemy flip after if the check is no ground ahead
				this._flip ();
			}
			
		} else {
			this._animator.SetInteger("AnimState", 0);
		}
	}

	void OnCollisionEnter2D(Collision2D otherCollider){
		if(otherCollider.gameObject.CompareTag ("Arrow")) {
			Destroy (gameObject);
		}

		if (otherCollider.gameObject.CompareTag ("Player")) {
			playerCollider.LifeCheck (minusDmg);
			Destroy (gameObject);
		}
	}
	
	void OnCollisionStay2D(Collision2D otherCollider){				// check if grounded when idle CollisionStay
		if (otherCollider.gameObject.CompareTag ("Ground")) {
			this._isGrounded = true;
		}
	}
	
	// when enemy moves it checks when the colliders are not touching anymore CollisionExit
	void OnCollisionExit2D(Collision2D otherCollider){				
		if (otherCollider.gameObject.CompareTag ("Ground")) {
			this._isGrounded = true;
		}
	}
	
	private void _flip(){
		if (this._transform.localScale.x == 1){
			this._transform.localScale= new Vector3 (-1f,1f,1f); // enemy already going left so you need the - 
			
		}else {
			this._transform.localScale= new Vector3 (1f,1f,1f); // reset to normal scale
		}
	}
}
