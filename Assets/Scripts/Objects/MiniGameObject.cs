using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameObject : MonoBehaviour {
    public Inventory.InventoryItems item;
    public bool activated = false;
    public bool achievedGoal = false;
    public float timeMax = 5;
    public int counterMax = 20;
    public bool refreshTimeMax = true;
    float timeLeft;
    int counter;

    void Start()
    {

    }

    void Update()
    {
        if (activated && !achievedGoal && Inventory.GetCurrentItemType() == item && !MissionManager.instance.paused && !MissionManager.instance.blocked)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Z)) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
            {
                MissionManager.instance.pausedObject = true;
                timeLeft = timeMax;
                //spritesheet
            }
            else if (Input.GetKeyDown(KeyCode.Z) || timeLeft <= 0)
            {
                MissionManager.instance.pausedObject = false;
                timeLeft = 0;
                if (refreshTimeMax) counter = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                counter++;
                //sprisheet
            }

            if(counter >= counterMax)
            {
                achievedGoal = true;
                timeLeft = counter = 0;
                MissionManager.instance.pausedObject = false;
            }
        }

    }
}
