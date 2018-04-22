using System.Collections.Generic;
using UnityEngine;

public class MinionEmmitter : MonoBehaviour {

    public int numMinions = 5;
    public float limitX0 = -3f, limitXF = 3f, limitY0 = -2f, limitYF = 2f;
    
    private void Start()
    {
        GenerateMinionMap();
    }

    private void Update()
    {

    }
    
    private void GenerateMinionMap()
    {
        for (int i = 0; i < numMinions; i++)
        {
            AddMinion();
        }
    }

    private void AddMinion()
    {
        GameObject minion = MissionManager.instance.AddObjectWithParent(
            "NPCs/minion", "", new Vector3(transform.position.x, transform.position.y, 0), new Vector3(1f, 1f, 1f), transform);
        minion.GetComponent<Minion>().speed = Random.Range(0.15f, 0.3f);
        minion.GetComponent<Minion>().timeMaxPower = Random.Range(2f, 5f);
        minion.GetComponent<Minion>().timeMaxChangeVelocity = Random.Range(4f, 8f);
        minion.GetComponent<Minion>().factorDivideSpeed = Random.Range(1.5f, 2f);
        minion.GetComponent<Minion>().timeInvertControls = Random.Range(4f, 8f);

        int numTargets = Random.Range(2, 5);
        Vector3[] targets = new Vector3[numTargets];
        for (int i = 0; i < numTargets - 1; i++)
        {
            targets[i] = new Vector3(Random.Range(limitX0, limitXF), Random.Range(limitY0, limitYF), 0);
        }
        targets[numTargets-1] = new Vector3(transform.position.x, transform.position.y, 0);
        minion.GetComponent<Minion>().targets = targets;
    }

}
