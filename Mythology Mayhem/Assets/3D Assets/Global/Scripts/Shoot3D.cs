using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot3D : MonoBehaviour
{
    GameManager gameManager;
    public GameObject ArrowPrefab;
    public Transform ArrowSpawn;
    float m_timestamp = 0f;
    [SerializeField] float attackRate = 3f;
    public AudioSource audioSource;
    public float AS = 0f;
    public float AL = 0f;
    public bool canShoot = true;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    void Update()
    {
        if (!gameManager.gameData.collectedBow) return;
        if (Time.timeScale != 1) return;
        if (!canShoot) return;

        if (m_timestamp >= attackRate)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                m_timestamp = 0;
                Shoot();
                audioSource.Play();
            }
        }
        else m_timestamp += Time.deltaTime;
    }

    public void Shoot()
    {
        float x = Screen.width / 2f;
        float y = Screen.height / 2f;

        var ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));

        ArrowSpawn.LookAt(ray.GetPoint(10), ArrowSpawn.up);
        GameObject arrow = Instantiate(ArrowPrefab, ArrowSpawn.position, ArrowSpawn.rotation);
        Rigidbody rb = arrow.GetComponentInChildren<Rigidbody>();

        //rb.velocity = ray.direction * AS;
        rb.velocity = arrow.transform.forward * AS;

        Destroy(arrow, AL);
    }
}
