using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AegirWaveScript : MonoBehaviour
{

    public float speed;

    public int damage;

    public ParticleSystem waveBuildup;
    public ParticleSystem waveProjectile;
    public bool buildupComplete;

    // Start is called before the first frame update
    void Start()
    {
        waveProjectile.gameObject.SetActive(false);
        waveBuildup.gameObject.SetActive(true);
        waveBuildup.Play();
        buildupComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (!waveBuildup.isPlaying && !buildupComplete) 
        {
            waveBuildup.gameObject.SetActive(false);
            waveProjectile.gameObject.SetActive(true);
            buildupComplete = true;
        }
    }

    public void ColliderTrigger(Collider other) 
    { 
        
    }
}
