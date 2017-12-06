using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour {
	public RPGTalk rpgTalk;

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
