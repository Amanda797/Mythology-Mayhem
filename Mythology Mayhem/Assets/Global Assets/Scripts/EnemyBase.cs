using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public enum EnemyState {idle, moving, attacking, dead};
    EnemyState enemyState;
    [Header("Animator")]
    [SerializeField] Animator anim;
    [SerializeField] string attackBoolName;
    [SerializeField] bool is2D;
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float idleTimer;
    [SerializeField] SpriteRenderer sr;
    bool idle;
    [Header("Attack")]
    [SerializeField] Collider2D attackTrigger;
    [SerializeField] Collider2D bodyTrigger;
    [SerializeField] Transform playerTarget;
    [SerializeField] Enemy enemy;
    bool playerDetected;
    bool damageTaken;
    [Header("Navigation")]
    [SerializeField] Transform[] waypoints;
    [SerializeField] int waypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy = gameObject.GetComponent<Enemy>();
        anim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        enemyState = EnemyState.idle;
        damageTaken = false;
        playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyState != EnemyState.dead) {
            //Sensing
            playerDetected = PlayerDetection();

            //Actions



        } else {
            //death
        }
        
    }//end update

    IEnumerator Idle() {
        yield return new WaitForSeconds(idleTimer);
        waypointIndex++;
        idle = false;
    }

    void Movement() {
        if(waypointIndex < waypoints.Length) {
            transform.position = Vector3.Lerp(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);
            //Flip Sprite Renderer
            if(Mathf.Sign(transform.position.x - waypoints[waypointIndex].position.x) > 0) {
                sr.flipX = true;
            } else if(Mathf.Sign(transform.position.x - waypoints[waypointIndex].position.x) < 0) {
                sr.flipX = false;
            }
        } else {
            waypointIndex = 0;
        }

        if(Vector3.Distance(transform.position, waypoints[waypointIndex].position) < 1f && !idle) {
            idle = true;
            StartCoroutine(Idle());
        }
    }

    void MainAbility() {
        enemy.Attack(playerTarget.gameObject);
    }

    void SpecialAbility() {
        
    }

    bool PlayerDetection() {
        if(attackTrigger.IsTouching(playerTarget.GetComponent<Collider2D>())) {
            transform.position = Vector3.Lerp(transform.position, playerTarget.GetComponent<Transform>().position, speed * Time.deltaTime);
            if(is2D) {
                Flip2D();
            }

            if(bodyTrigger.IsTouching(playerTarget.GetComponent<Collider2D>())) {
                if(enemy.CanAttack) {
                    anim.SetBool(attackBoolName, true);
                    return true;
                } else {
                    anim.SetBool(attackBoolName, false);
                    return false;
                }
            } else {
                return false;
            }
        } else {
            return false;
        }
    }//end player detection

    void Flip2D() {
        //Flip Sprite Renderer
        if(Mathf.Sign(transform.position.x - waypoints[waypointIndex].position.x) > 0) {
            sr.flipX = false;
        } else if(Mathf.Sign(transform.position.x - waypoints[waypointIndex].position.x) < 0) {
            sr.flipX = true;
        }
    }
}
