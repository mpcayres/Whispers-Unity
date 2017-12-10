using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour{
    public static int goodSpiritGardenKilled = 0;
    public static int evilSpiritGardenKilled = 0;

    MissionManager missionManager;
    public bool canSummom = false;
    private bool active = false;
    public GameObject[] goodSpiritVector;

    private void Start()
    {
        missionManager = GameObject.Find("Player").GetComponent<MissionManager>();
    }

    private void Update()
    {
        print(active);
        print(missionManager.invertWorld);
        print(goodSpiritVector.Length);

        if (!active && missionManager.invertWorld)
        {
            for(int i = 0; i< goodSpiritVector.Length; i++)
            {
                goodSpiritVector[i].SetActive(true);
                print("A");
            }
            active = true;
        }
        else if (active && !missionManager.invertWorld)
        {
            for (int i = 0; i < goodSpiritVector.Length; i++)
            {
                goodSpiritVector[i].SetActive(false);
                print("B");
            }
            active = false;
        }
    }
     
}
