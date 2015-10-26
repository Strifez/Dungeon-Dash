using UnityEngine;
using System.Collections;

//Jason Huang 300818592
//Source: Professor Tom's Mail Pilot/Unity Website
//Last Modified: Oct,26,2015
//Description: Spike Damage script 
public class HurtPlayer : MonoBehaviour {

	public int minusFullDmg=3;
	public int lifeValue=3;

	private PlayerCollider playerCollider; 

	// Use this for initialization
	void Start () {
		GameObject playerColliderObject = GameObject.FindWithTag("Player"); // allows us to pull LifeCheck Method from the player collider script
		if (playerColliderObject != null)
		{
			playerCollider = playerColliderObject.GetComponent<PlayerCollider>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D otherCollider){
			if (otherCollider.tag == "Player") { 	//Spike Trigger, if its a player then ..
				playerCollider.LifeCheck (minusFullDmg); // minus 3 life for spike damage 
			}
		}

}




