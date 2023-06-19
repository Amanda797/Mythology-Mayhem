using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shoot2D : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public Transform ArrowSpawn;
    public float TBS = 0f;
    private float m_timestamp = 0f;
    public AudioSource source;
    public float AS = 0f;
    public float AL = 0f;
    public GameObject player;
    public bool CS = true;

    void Update()
    {
        /*if(SceneManager.GetActiveScene().name == "2Dlabyrinth_Levers 1" || SceneManager.GetActiveScene().name == "2Dlabyrinth_Pedastals")
        {
            CS = false;
        }*/
        if((Time.time >= m_timestamp) && (Input.GetKeyDown(KeyCode.Mouse1)) && player.GetComponent<PlayerController>().pushing == false && CS == true)
        {
            Shoot();
            m_timestamp = Time.time + TBS;
            source.Play();
        }
    }

    public void Shoot()
    {
        var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, ArrowSpawn.rotation);
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
