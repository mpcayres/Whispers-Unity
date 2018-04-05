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
                if (!goodDestroyed[i])
                {
                    goodSpiritVector[i].SetActive(false);
                }
                if (!evilDestroyed[i])
                {
                    evilSpiritVector[i].SetActive(false);
                }
            }
            active = false;
        }
    }

    public static void GenerateSpiritMap()
    {
        Spirit.maxEvilKilled = 4;

        float x = -2.1f, y = 0.8f;
        float maxX = 12;
        int sequencia = 0, aux2 = -1;
        for (int j = 0; j < 4; j++)
        {
            if (j == 1)
            {
                maxX = 18;
            }
            for (int i = 0; i < maxX; i++)
            {

                int aux = Random.Range(0, 4);
                if (aux2 == aux)
                {
                    sequencia++;
                    if (sequencia == 3)
                    {
                        aux = (aux + 1) % 4;
                    }
                }
                else
                {
                    sequencia = 0;
                }
                switch (aux)
                {
                    case 1:
                        MissionManager.instance.AddObject("Scenery/GoodSpiritAux", "", new Vector3(x, y, 0), new Vector3(1f, 1f, 1));
                        break;
                    case 2:
                        MissionManager.instance.AddObject("Scenery/EvilSpiritAux", "", new Vector3(x, y, 0), new Vector3(1f, 1f, 1));
                        break;
                    default:
                        MissionManager.instance.AddObject("Scenery/KillerSpirit", "", new Vector3(x, y, 0), new Vector3(1f, 1f, 1));
                        break;
                }
                aux2 = aux;
                x += 0.9f;
            }
            y -= 1f;
            x = -7.5f;
        }
        Spirit.newHealth = 2;
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

    public static void RefreshSpirits()
    {
        bool[] goodDestroyedAux = { false, false, false, false, false };
        bool[] evilDestroyedAux = { false, false, false, false, false };
        goodDestroyed = goodDestroyedAux;
        evilDestroyed = evilDestroyedAux;

        goodSpiritGardenKilled = 0;
        evilSpiritGardenKilled = 0;
    }

}
