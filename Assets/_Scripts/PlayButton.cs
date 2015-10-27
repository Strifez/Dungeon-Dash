using UnityEngine;
using System.Collections;

//Jason Huang 300818592
//Source: Professor Tom's Mail Pilot
//Last Modified: Oct,26,2015
//Description: Play Button Interaction 

public class PlayButton : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnStartButtonClick() {
		Application.LoadLevel("main");
		
	}
}
