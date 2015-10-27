using UnityEngine;
using System.Collections;

//Jason Huang 300818592
//Source: Professor Tom's Mail Pilot/Unity Website
//Last Modified: Oct,26,2015
//Description: Coin Destroyer Script

public class CoinController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D (Collider2D otherCollider)
	{
		if (otherCollider.gameObject.CompareTag ("Player")) {
			Destroy (gameObject);
		}
	}
}
