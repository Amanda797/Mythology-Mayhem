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

    public float despawnTimer;

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

        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0) 
        {
            Destroy(this.gameObject);
        }
    }

    public void ColliderTrigger(Collider other) 
    {
        if (other.tag == "Ship") 
        {
            ShipScript ship = other.gameObject.GetComponent<ShipScript>();
            if (ship != null) 
            {
                ship.anim.SetTrigger("Wave");
            }
        }
    }
}
