using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenses : MonoBehaviour
{
    [Header("World")]
    public GameObject playerGO;

    [Header("Stats")]
    [SerializeField] float maxHealth;
    public float currHealth;

    [Header("Body")] //Touch
    public Transform enemyBody; //The main enemy and the body trigger
    public Animator anim;
    public SpriteRenderer sr;
    public bool playerCollided;

    [Header("Sight")]
    public Transform sightTrigger;
    public bool playerDetected;

    [Header("Sound")]
    public AudioSource voicebox;
    public AudioClip [] voices;

    // Start is called before the first frame update
    void Start()
    {
        //World
        playerGO = GameObject.FindGameObjectWithTag("Player");
        //Stats
        currHealth = maxHealth;
        //Body, Sight, Sound
        if(enemyBody == null) {
            Debug.LogWarning("Enemy Body (Transform with Box Collider 2D) not set.");
        } else {
            if(anim == null) {
                anim = enemyBody.GetComponent<Animator>();
            }
            if(sr == null) {
                sr = enemyBody.GetComponent<SpriteRenderer>();
            }
            if(voicebox == null) {
                voicebox = enemyBody.GetComponent<AudioSource>();
            }
        }
        if(sightTrigger == null) {
            Debug.LogWarning("Sight Trigger (Transform with Box Collider 2D) not set.");
        }
        playerCollided = false;
        playerDetected = false;
    }//end start

    // Update is called once per frame
    void Update()
    {
        //Sight Sensing
        if(sightTrigger.GetComponent<BoxCollider2D>().IsTouching(playerGO.GetComponent<Collider2D>())) {
            playerDetected = true;
        } else {
            playerDetected = false;
        }

        //Body Sensing
        if(enemyBody.GetComponent<CircleCollider2D>().IsTouching(playerGO.GetComponent<Collider2D>())) {
            playerCollided = true;
        } else {
            playerCollided = false;
        }
    }//end update
}
