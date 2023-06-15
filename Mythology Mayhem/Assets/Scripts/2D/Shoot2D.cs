using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot2D : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public Transform ArrowSpawn;
    public float TBS = 0f;
    private float m_timestamp = 0f;
    public AudioSource source;
    public float AS = 0f;
    public float AL = 0f;
    

    // Update is called once per frame
    void Update()
    {
        if((Time.time >= m_timestamp) && (Input.GetKeyDown(KeyCode.Mouse1)))
        {
            Shoot();
            m_timestamp = Time.time + TBS;
            source.Play();
        }
    }

    public void Shoot()
    {
        var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, ArrowSpawn.rotation);
        /*arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(AS, 0.0f); arrow.transform.forward * AS;*/
        if(gameObject.GetComponent<PlayerStats>().flipped == false)
        {
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(AS, 0.0f);
        }
        else 
        {
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-AS, 0.0f);
        }

        Destroy(arrow, AL);
    }
}
