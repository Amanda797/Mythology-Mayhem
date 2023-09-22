using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsAI3D : EnemyAI3D
{
    [SerializeField] GameObject SnowballPrefab;
    [SerializeField] float throwingForce = 300f;
    Transform playerTransform;
    Vector3 lastPlayerPosition;

    public override void Idle() {
        if(anim != null)
            anim.SetBool(walkingBool, false);
	    agent.speed = 0f;
	    idleTimer += Time.deltaTime;
        if(idleTimer >= idleDuration) {
            idleTimer = 0f;
            agent.speed = patrolSpeed;
            isIdle = false;
            agent.isStopped = false;
            Move();
        }
    }//end idle

    public override void Walking() {
        if(anim != null)
            anim.SetBool(walkingBool, true);
        agent.speed = patrolSpeed;
        //Going to target
        if (Vector3.Distance(transform.position, target) > 2) {
            agent.SetDestination(target);
        }
        //Reached target
        else {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            isIdle = true;
        }
    }//end walking

    public override void Attack() {
        //Is there a detected player? If so, attack routine

        if(player != null)
        {
            playerTransform = player.gameObject.transform;
            if(playerTransform.position.x - lastPlayerPosition.x > 0 || playerTransform.position.x - lastPlayerPosition.x < 0) {
                //Player has moved within eyesight, attack
                if(anim != null) {
                    anim.SetBool(runningBool, false);
                    anim.SetBool(walkingBool, false);
                }

                if(attackTimer >= attackDuration) {
                    //Throw Snowball
                    GameObject snowball = Instantiate(SnowballPrefab, transform.position, Quaternion.LookRotation(playerTransform.position));
                    if(snowball.GetComponent<Rigidbody>()) {
                        snowball.GetComponent<Rigidbody>().AddForce(throwingForce * Vector3.forward, ForceMode.Impulse);
                    }
                    //Set Animation Trigger
                    if(anim != null)
                        anim.SetTrigger(attackTrigger);
                    //player.gameObject.GetComponent<FPSHealth>().TakeDamage(attackDamage); Check for damage on player go
                    attackTimer = 0f;
                }
                //Wait to attack again
                else { 
                    agent.ResetPath();
                    agent.speed = 0f;
                    attackTimer += Time.deltaTime;
                }//end timer check 
            } else {
                //Do nothing, player has not been "seen" moving
            }
            lastPlayerPosition = playerTransform.position;
        }

    }//end attack

    public override void OnTriggerEnter(Collider col) {
        //Do nothing
    }//end on trigger enter

    public override void OnTriggerExit(Collider col) {
        //Do nothing
    }//end on trigger exit
}
