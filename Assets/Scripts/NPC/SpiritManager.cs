using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour{
    public static int goodSpiritGardenKilled = 0;
    public static int evilSpiritGardenKilled = 0;

    static MissionManager missionManager;
    public static bool canSummom = false;
    private bool active = false;
    public GameObject[] goodSpiritVector;
    public GameObject[] evilSpiritVector;
    private static bool[] goodDestroyed = { false, false, false, false, false };
    private static bool[] evilDestroyed = { false, false, false, false, false };

    private void Start()
    {
        missionManager = GameObject.Find("Player").GetComponent<MissionManager>();
    }

    private void Update()
    {
        if (!active && missionManager.invertWorld && canSummom)
        {
            for(int i = 0; i < goodSpiritVector.Length; i++)
            {
                if (!goodDestroyed[i])
                    goodSpiritVector[i].SetActive(true);
                if (!evilDestroyed[i])
                    evilSpiritVector[i].SetActive(true);
            }
            active = true;
        }
        else if (active && !missionManager.invertWorld)
        {
            for (int i = 0; i < goodSpiritVector.Length; i++)
            {
                if(!goodDestroyed[i])
                    goodSpiritVector[i].SetActive(false);
                if (!evilDestroyed[i])
                    evilSpiritVector[i].SetActive(false);
            }
            active = false;
        }
    }

    public static void DestroyGoodSpirit(int number)
    {
        goodDestroyed[number] = true;
        goodSpiritGardenKilled++;
    }

    public static void DestroyEvilSpirit(int number)
    {
        evilDestroyed[number] = true;
        evilSpiritGardenKilled++;

        if(evilSpiritGardenKilled >= 3)
        {
            missionManager.GameOver();
        }
    }

}
