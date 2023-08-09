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
    private bool CSU = false;
    private bool CSN = false;
    [SerializeField] private float ASx = 0f;
    [SerializeField] private float ASy = 0f;
    [SerializeField] private float AD = 0f;
    [SerializeField] private float ADi = 0f;
    private bool CSD = false;

    [SerializeField] private Animator anim;
    [SerializeField] public PlayerStats_SO ps;

    void Start()
    {
        CS = false;
        source = gameObject.GetComponent<AudioSource>();

        HealthUIController huic = GameObject.FindGameObjectWithTag("huic").GetComponent<HealthUIController>();
        if(huic != null) { 
            ps = huic.ps;
            ps.CanAttack = true;
            ps.NextAttackTime = 0;
            ps.CurrHealth = ps.MaxHealth;
        }
        else{
            print("Can't find huic's player stats so");
        }
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

            ps.CanAttack = false;
        }
        if (CS == false)
        {
            anim.SetBool("CanShoot", false);
            anim.SetBool("UseSword", true);

            ps.CanAttack = true;
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
            CSD = false;
            anim.SetBool("CanShootUpward", true);
        }
        else if (up == 0f)
        {
            CSU = false;
            CSD = false;
            anim.SetBool("CanShootUpward", false);
            anim.SetBool("CanShootDownward", false);
        }
        if(up == -1f)
        {
            CSD = true;
            CSU = false;
            anim.SetBool("CanShootDownward", true);
        }
        if (CSU == true || CSD == true)
        {
            CSN = false;
        }
        else if (CSU == false || CSD == false)
        {
            CSN = true;
        }
        if ((Time.time >= m_timestamp) && (Input.GetKeyDown(KeyCode.Mouse0)) && player.GetComponent<PlayerController>().pushing == false && CS == true && CSU == true && CSN == false && CSD == false)
        {
            ShootUp();
            m_timestamp = Time.time + TBS;
            source.Play();
        }
        if ((Time.time >= m_timestamp) && (Input.GetKeyDown(KeyCode.Mouse0)) && player.GetComponent<PlayerController>().pushing == false && CS == true && CSD == true)
        {
            ShootDown();
            m_timestamp = Time.time + TBS;
            source.Play();
        }
    }

    public void Shoot()
    {
        anim.SetTrigger("Shoot");
        if (gameObject.GetComponent<PlayerStats>().flipped == false)
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, ArrowSpawn.rotation);
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(AS, 0.0f);
            Destroy(arrow, AL);
        }
        else
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 180, 0));
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-AS, 0.0f);
            Destroy(arrow, AL);
        }
    }
    public void ShootUp()
    {
        anim.SetTrigger("ShootUpward");
        if (up > 0.01f && CSU == true && gameObject.GetComponent<PlayerStats>().flipped == false && CSD == false)
        {
            var arrow2 = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 0, AD));
            arrow2.GetComponent<Rigidbody2D>().velocity = new Vector2(ASx, ASy);
            Destroy(arrow2, AL2);
        }
        if (up > 0.01f && CSU == true && gameObject.GetComponent<PlayerStats>().flipped == true && CSD == false)
        {
            var arrow2 = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 180, AD));
            arrow2.GetComponent<Rigidbody2D>().velocity = new Vector2(-ASx, ASy);
            Destroy(arrow2, AL2);
        }
    }
    public void ShootDown()
    {
        anim.SetTrigger("ShootDownward");
        if (up < 0f && CSU == false && gameObject.GetComponent<PlayerStats>().flipped == false && CSD == true)
        {
            var arrow3 = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 0, ADi));
            arrow3.GetComponent<Rigidbody2D>().velocity = new Vector2(ASx, -ASy);
            Destroy(arrow3, AL2);
        }
        if (up < 0f && CSU == false && gameObject.GetComponent<PlayerStats>().flipped == true && CSD == true)
        {
            var arrow3 = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 180, ADi));
            arrow3.GetComponent<Rigidbody2D>().velocity = new Vector2(-ASx, -ASy);
            Destroy(arrow3, AL2);
        }
    }
}
