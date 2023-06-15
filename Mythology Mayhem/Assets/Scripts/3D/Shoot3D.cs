using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot3D : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public Transform ArrowSpawn;
    public float TBS = 0f;
    private float m_timeStamp = 0f;
    public AudioSource source;
    public float AS = 0f;

    void OnAwake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time >= m_timeStamp) && (Input.GetKeyDown(KeyCode.Mouse0)))
        {
            Shoot();
            m_timeStamp = Time.time + TBS;
            source.Play();
        }
    }

    public void Shoot()
    {
        var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, ArrowSpawn.rotation);

        arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * AS;

        Destroy(arrow, 2.0f);
    }
}
