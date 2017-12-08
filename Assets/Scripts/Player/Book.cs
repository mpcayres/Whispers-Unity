using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour {

    private bool[] pages = { false, false, false, false, false };
    int pageShowing; // 0 - 2
    bool isActive;

    MissionManager missionManager;

	void Start () {
        isActive = false;
        pageShowing = 0;
        missionManager = GameObject.Find("Player").GetComponent<MissionManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.J) && !MissionManager.instance.blocked && !MissionManager.instance.pausedObject)
        {
            isActive = true;
        }
        if (isActive == true)
        {
            isActive = false;
        }

    }

}
