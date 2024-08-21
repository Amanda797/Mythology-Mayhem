using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shoot2D : MonoBehaviour
{
    GameManager gameManager;
    PlayerController playerController;
    PlayerStats playerStats;
    Animator anim;

    public PlayerStats_SO ps;
    public AudioSource audioSource;
    public GameObject ArrowPrefab;
    public Transform ArrowSpawn;

    private float m_timestamp = 0f;
    public float AS = 0f;
    public float AL = 0f;
    public float AL2 = 0f;
    private bool canShoot = false;
    private float aim = 0f;
    private bool canShootUp = false;
    private bool canShootDown = false;
    private bool canShootAhead = false;
    [SerializeField] private float ASx = 0f;
    [SerializeField] private float ASy = 0f;
    [SerializeField] private float AD = 0f;
    [SerializeField] private float ADi = 0f;


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();
        anim = GetComponent<Animator>();
        gameManager = GameManager.instance;
    }
    void Update()
    {
        if (playerController.pushing) return;
        if (playerController.climbing) return;
        if (!gameManager.gameData.collectedBow) return;
        if (Time.timeScale != 1) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            canShoot = !canShoot;

            anim.SetBool("CanShoot", canShoot);
            anim.SetBool("UseSword", !canShoot);

            ps.CanAttack = !canShoot;
        }

        if (!canShoot) return;

        aim = Input.GetAxisRaw("Vertical");

        canShootUp = aim > 0;
        canShootDown = aim < 0;
        canShootAhead = aim == 0;

        anim.SetBool("CanShootUpward", canShootUp);
        anim.SetBool("CanShootDownward", canShootDown);

        if (Time.time >= m_timestamp && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
            m_timestamp += Time.deltaTime;
            audioSource.Play();
        }
    }

    public void Shoot()
    {
        GameObject arrow = Instantiate(ArrowPrefab, ArrowSpawn.position, ArrowSpawn.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        if (aim == 0)
        {
            anim.SetTrigger("Shoot");
            Destroy(arrow, AL);

            if (playerStats.flipped)
            {
                arrow.transform.rotation = Quaternion.Euler(0, 180, 0);
                rb.velocity = new Vector2(-AS, 0.0f);
            }
            else rb.velocity = new Vector2(AS, 0.0f);
        }
        else if (aim > 0)
        {
            anim.SetTrigger("ShootUpward");
            arrow.transform.rotation = Quaternion.Euler(0, 0, AD);
            Destroy(arrow, AL2);

            if (playerStats.flipped)
            {
                arrow.transform.rotation = Quaternion.Euler(0, 180, AD);
                rb.velocity = new Vector2(-ASx, ASy);
            }
            else rb.velocity = new Vector2(ASx, ASy);
        }
        else if (aim < 0)
        {
            anim.SetTrigger("ShootDownward");
            arrow.transform.rotation = Quaternion.Euler(0, 0, ADi);
            Destroy(arrow, AL2);

            if (playerStats.flipped)
            {
                arrow.transform.rotation = Quaternion.Euler(0, 180, ADi);
                rb.velocity = new Vector2(-ASx, -ASy);
            }
            else rb.velocity = new Vector2(ASx, -ASy);
        }
    }
}
