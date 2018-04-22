using System.Collections.Generic;
using UnityEngine;

public class MinionEmmitter : MonoBehaviour {

    public float limitX0 = -3f, limitXF = 3f, limitY0 = -2f, limitYF = 2f;
    
    private void Start()
    {
    }

    private void Update()
    {

    }
    
    public void GenerateSpiritMap()
    {
        
    }

    private void AddMinion()
    {
        GameObject minion = MissionManager.instance.AddObjectWithParent("NPCs/minion", "", new Vector3(0, 0, 0), new Vector3(1f, 1f, 1f), transform);
        minion.GetComponent<Minion>().timeMaxPower = Random.Range(2f, 5f);
        minion.GetComponent<Minion>().timeMaxChangeVelocity = Random.Range(4f, 8f);
        minion.GetComponent<Minion>().factorDivideSpeed = Random.Range(1.5f, 2f);
        minion.GetComponent<Minion>().timeInvertControls = Random.Range(4f, 8f);

        List<Transform> targets = new List<Transform>();
        int numTargets = Random.Range(2, 5);
        for (int i = 0; i < numTargets; i++)
        {
            //targets.Add(new Transform()); // -> Vector3[]
        }

    }

}
