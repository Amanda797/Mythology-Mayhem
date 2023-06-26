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
    public float AL2 = 0f;
    public GameObject player;
    private bool CS = false;
    private float up = 0f;
    public bool CSU = false;
    private bool CSN = false;
    [SerializeField] private float ASx = 0f;
    [SerializeField] private float ASy = 0f;
    [SerializeField] private float AD = 0f;
    [SerializeField] private float ADi = 0f;

    [SerializeField] private Animator anim;

    void OnStart()
    {
        CS = false;
    }
    void Update()
    {
        up = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.X) && CS == true || Input.GetKeyDown(KeyCode.X) && CS == false)
        {
            CS = !CS;
        }
        if (CS == true)
        {
            anim.SetBool("CanShoot", true);
            anim.SetBool("UseSword", false);
        }
        if (CS == false)
        {
            anim.SetBool("CanShoot", false);
            anim.SetBool("UseSword", true);
        }
        /*if(SceneManager.GetActiveScene().name == "2Dlabyrinth_Levers 1" || SceneManager.GetActiveScene().name == "2Dlabyrinth_Pedastals")
        {
            CS = false;
        }*/
        if ((Time.time >= m_timestamp) && (Input.GetKeyDown(KeyCode.Mouse0)) && player.GetComponent<PlayerController>().pushing == false && CS == true && CSN == true)
        {
            Shoot();
            m_timestamp = Time.time + TBS;
            source.Play();
        }
        if (up == 1f)
        {
            CSU = true;
            anim.SetBool("CanShootUpward", true);
        }
        else if (up < 1f)
        {
            CSU = false;
            anim.SetBool("CanShootUpward", false);
        }
        if (CSU == true)
        {
            CSN = false;
        }
        else if (CSU == false)
        {
            CSN = true;
        }
        if ((Time.time >= m_timestamp) && (Input.GetKeyDown(KeyCode.Mouse0)) && player.GetComponent<PlayerController>().pushing == false && CS == true && CSU == true && CSN == false)
        {
            ShootUp();
            m_timestamp = Time.time + TBS;
            source.Play();
        }
    }

    public void Shoot()
    {
        anim.SetTrigger("Shoot");
        var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, ArrowSpawn.rotation);
        if (gameObject.GetComponent<PlayerStats>().flipped == false)
        {
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(AS, 0.0f);
        }
        else
        {
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-AS, 0.0f);
        }
        Destroy(arrow, AL);
    }
    public void ShootUp()
    {
        anim.SetTrigger("ShootUpward");
        if (up > 0.01f && CSU == true && gameObject.GetComponent<PlayerStats>().flipped == false)
        {
            var arrow2 = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 0, AD));
            arrow2.GetComponent<Rigidbody2D>().velocity = new Vector2(ASx, ASy);
            Destroy(arrow2, AL2);
        }
        if (up > 0.01f && CSU == true && gameObject.GetComponent<PlayerStats>().flipped == true)
        {
            var arrow2 = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 0, ADi));
            arrow2.GetComponent<Rigidbody2D>().velocity = new Vector2(-ASx, ASy);
            Destroy(arrow2, AL2);
        }
        //Destroy(arrow2, AL);
    }
}
