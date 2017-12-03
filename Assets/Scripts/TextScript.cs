using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using RPGTALK;

public class TextScript : MonoBehaviour {
	public RPGTalk rpgTalk;
	private int count = 0;
	void Update () {


	}


	void OnMadeChoice(int questionId, int choiceID){
		if ( questionId == 0) {
			if (choiceID == 0) {
				MissionManager.instance.pathBird++;
			} else {
				MissionManager.instance.pathCat++;
			}
		}
	}


}
