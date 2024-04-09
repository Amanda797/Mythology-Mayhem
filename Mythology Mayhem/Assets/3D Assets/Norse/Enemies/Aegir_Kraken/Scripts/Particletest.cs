using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Particletest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem system = new ParticleSystem();
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[0];
        system.GetParticles(particles);

        particles[0].remainingLifetime = 0;
    }
}
