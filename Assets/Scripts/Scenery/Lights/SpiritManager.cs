using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour {
    private static int goodSpiritKilled = 0;
    private static int evilSpiritKilled = 0;
    private static int killerSpiritKilled = 0;

    private static int maxEvilKilled = 4;
    private static int maxKillerKilled = 6;

    private Dictionary<int, GameObject> goodSpiritDictionary = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> evilSpiritDictionary = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> killerSpiritDictionary = new Dictionary<int, GameObject>();

    private void Start()
    {
        goodSpiritKilled = 0;
        evilSpiritKilled = 0;
        killerSpiritKilled = 0;
    }

    private void Update()
    {

    }

    public void GenerateSpiritMap()
    {
        float x = -2.1f, y = 0.8f;
        float maxX = 12;
        int sequencia = 0, aux2 = -1;
        int goodSpiritCount = 0, evilSpiritCount = 0, killerSpiritCount = 0;

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
                        GameObject goodSpirit = MissionManager.instance.AddObjectWithParent("Scenery/GoodSpirit", "", new Vector3(x, y, 0), new Vector3(1f, 1f, 1), transform);
                        goodSpiritDictionary.Add(goodSpiritCount, goodSpirit);
                        goodSpirit.GetComponent<Spirit>().number = goodSpiritCount;
                        goodSpiritCount++;
                        break;
                    case 2:
                        GameObject evilSpirit = MissionManager.instance.AddObjectWithParent("Scenery/EvilSpirit", "", new Vector3(x, y, 0), new Vector3(1f, 1f, 1), transform);
                        evilSpiritDictionary.Add(evilSpiritCount, evilSpirit);
                        evilSpirit.GetComponent<Spirit>().number = evilSpiritCount;
                        evilSpiritCount++;
                        break;
                    default:
                        GameObject killerSpirit = MissionManager.instance.AddObjectWithParent("Scenery/KillerSpirit", "", new Vector3(x, y, 0), new Vector3(1f, 1f, 1), transform);
                        killerSpiritDictionary.Add(killerSpiritCount, killerSpirit);
                        killerSpirit.GetComponent<Spirit>().number = killerSpiritCount;
                        killerSpiritCount++;
                        break;
                }
                aux2 = aux;
                x += 0.9f;
            }
            y -= 1f;
            x = -7.5f;
        }
    }

    public static void DestroyGoodSpirit()
    {
        goodSpiritKilled++;
    }

    public static void DestroyEvilSpirit()
    {
        evilSpiritKilled++;

        if (evilSpiritKilled >= maxEvilKilled)
        {
            MissionManager.instance.GameOver();
        }
    }

    public static void DestroyKillerSpirit()
    {
        killerSpiritKilled++;

        if (killerSpiritKilled >= maxKillerKilled)
        {
            MissionManager.instance.GameOver();
        }
    }

}
