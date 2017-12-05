using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour {
    ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject TargetedParticle)
    {
        print("PARTCOL: " + TargetedParticle.tag);
        if (TargetedParticle.tag == "Player")
        {
            MissionManager.instance.GameOver();
        }
    }

}
