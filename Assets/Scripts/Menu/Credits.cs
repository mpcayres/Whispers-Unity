using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {
    // Use this for initialization
    public void ChangeScene (int number = 0) {
        MissionManager.LoadScene(0); // animation finished: load main menu
    }
}
