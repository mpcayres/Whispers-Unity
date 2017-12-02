using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using RPGTALK;

public class TextScript : MonoBehaviour {
	public RPGTalk rpgTalk;
	private int count = 0;
	void Update () {

		//skip the Talk to the end if the player hit Return
		if(Input.GetKeyDown(KeyCode.Return)){
			rpgTalk.EndTalk ();
		}

	}


	void OnMadeChoice(int questionId, int choiceID){
		if ( questionId == 0) {

		}
	}

	public void EraseLine(){
		count++;
		MissionManager.instance.rpgTalk.NewTalk ("EraseLine", "EraseLineEnd");
	}
}
