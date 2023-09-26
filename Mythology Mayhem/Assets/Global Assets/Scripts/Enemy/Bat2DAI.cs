using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat2DAI : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] Animator anim;
    [SerializeField] string attackBoolName;
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
    }

    // Update is called once per frame
    void Update()
    {
        if(attackTrigger.IsTouching(playerTarget.GetComponent<Collider2D>())) {
            transform.position = Vector3.Lerp(transform.position, playerTarget.GetComponent<Transform>().position, speed * Time.deltaTime);
            //Flip Sprite Renderer
            if(Mathf.Sign(transform.position.x - waypoints[waypointIndex].position.x) > 0) {
                sr.flipX = false;
            } else if(Mathf.Sign(transform.position.x - waypoints[waypointIndex].position.x) < 0) {
                sr.flipX = true;
            }

            if(bodyTrigger.IsTouching(playerTarget.GetComponent<Collider2D>())) {
                if(enemy.CanAttack) {
                    anim.SetBool(attackBoolName, true);
                    enemy.Attack(playerTarget.gameObject);
                } else {
                    anim.SetBool(attackBoolName, false);
                }
            }
        } else {
            if(!idle) {
                Move();
            }
        }
    }//end update

    void Move() {
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

    IEnumerator Idle() {
        yield return new WaitForSeconds(idleTimer);
        waypointIndex++;
        idle = false;
    }

}
