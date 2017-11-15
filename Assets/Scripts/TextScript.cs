using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using RPGTALK;

public class TextScript : MonoBehaviour {
	public RPGTalk rpgTalk;

	void Update () {

		//skip the Talk to the end if the player hit Return
		if(Input.GetKeyDown(KeyCode.Return)){
			rpgTalk.EndTalk ();
		}

	}


	// Use this for initialization
	public void GiveBackControls(){
		
	}

	void OnMadeChoice(int questionId, int choiceID){
		if ( questionId == 0) {

		}
	}
}
