using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour {

    private bool[] pages = { false, false, false, false, false };
    private int pageQuantity;
    private int pageShowing; // 0 - 2
  
    GameObject book;
    MissionManager missionManager;

	void Start () {
        pageQuantity = 0;
        book = GameObject.Find("HUDCanvas").transform.Find("Book").gameObject;
        pageShowing = 0;
        missionManager = GameObject.Find("Player").GetComponent<MissionManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.J) && !MissionManager.instance.blocked 
            && !MissionManager.instance.pausedObject)
        {
            ShowBook();
        }
    }

    void ShowBook()
    {
        if(book.activeSelf)
        {
            book.SetActive(false);
            missionManager.paused = false;
        }
        else
        {
            book.SetActive(true);
            missionManager.paused = true;

        }
    }

}
