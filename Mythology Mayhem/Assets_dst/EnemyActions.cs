using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    EnemySenses senses;

    [Header("Main Attack")]
    [SerializeField] float mainAttackDmg;
    bool mainAvailable;
    [SerializeField] float mainTimer;
    [SerializeField] string mainAttackName;

    [Header("Special Attack")]
    bool specialAvailable;
    [SerializeField] float specialTimer;
    [SerializeField] string specialAttackName;
    
    [Header("Quirk")]
    bool quirkAvailable;
    
    [Header("Movement")]
    List<Vector3> waypoints;
    [SerializeField] float speed;
    [SerializeField] float idleTimer;
    bool idle;
    [SerializeField] float travelDistance;
    int waypointIndex;
    [SerializeField] string movementName;
    [SerializeField] string hurtTriggerName;
    [SerializeField] string deathName;

    // Start is called before the first frame update
    void Start()
    {
        senses = gameObject.GetComponent<EnemySenses>();

        //Movement Init
        waypoints = new List<Vector3>();
        Vector3 pos = senses.enemyBody.transform.position;
        pos.x += travelDistance;
        waypoints.Add(pos);
        print(pos);
        pos = senses.enemyBody.transform.position;
        pos.x -= travelDistance;
        waypoints.Add(pos);
        print(pos);
        waypointIndex = -1;
        idle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(senses.currHealth > 0) {
            //Alive
            //Use Quirk if available
            if(quirkAvailable) {
                //Activate quirk
                Quirk();
            }

            //Check Senses
            if(senses.playerDetected) {
                //Move To Player
                if(!idle) {
                    MoveToTarget(senses.playerGO.transform.position);
                }
                if(specialAvailable) {
                    //Activate Special Ability
                    StartCoroutine(SpecialAbility_IE());
                }
                if(senses.playerCollided && mainAvailable) {
                    //MainAttack
                    StartCoroutine(MainAttack_IE());
                }
            } else {
                //Move to Waypoint
                if(!idle) {
                    if(waypointIndex >= -1) {
                        waypointIndex++;
                        if(waypointIndex >= waypoints.Count) {
                            waypointIndex = 0;
                        }
                    }
                    //MoveToTarget(waypoints[waypointIndex]);
                }
            }
        } else {
            //Dead
            Death();
        }
    }//end update

    void MoveToTarget(Vector3 target) {
        if(Vector3.Distance(senses.enemyBody.transform.position, target) > .1f) {
            senses.enemyBody.transform.position = Vector3.Lerp(senses.enemyBody.transform.position, target, speed * Time.deltaTime);
        } else {
            idle = true;
            StartCoroutine(Idle());
        }
        
    }//end move to target

    IEnumerator Idle() {
        senses.anim.SetBool(movementName, false);
        yield return new WaitForSeconds(idleTimer);
        idle = false;
        senses.anim.SetBool(movementName, true);
    }

    public void Quirk() {
        //Quirk Logic here
    }//end quirk

    IEnumerator MainAttack_IE() {
        //MainAttack
        if(mainAttackName != "") {
            senses.anim.SetBool(mainAttackName, true);
            MainAttack();
            mainAvailable = false;
            yield return new WaitForSeconds(mainTimer);
            mainAvailable = true;
        } else {
            Debug.LogWarning("Main Attack Bool string not set, execution failed...");
        }
    }//end main attack IEnumerator

    IEnumerator SpecialAbility_IE() {
        //Special Ability
        if(mainAttackName != "") {
            senses.anim.SetBool(specialAttackName, true);
            SpecialAbility();
            specialAvailable = false;
            yield return new WaitForSeconds(specialTimer);
            specialAvailable = true;
        } else {
            Debug.LogWarning("Special Attack Bool string not set, execution failed...");
        }
    }//end special ability IEnumerator

    public void MainAttack() {
        senses.playerGO.GetComponent<Health>().TakeDamage(mainAttackDmg);
    }// main attack void

    public void SpecialAbility() {
        //Customizable Special Ability
        Debug.Log("Special Ability Customizable");
    }//end special ability void

    public void TakeDamage(float amount) {
        senses.anim.SetTrigger(hurtTriggerName);
        senses.currHealth -= amount;
    }//end take damage

    void Death() {
        //Death
        senses.anim.SetBool(deathName, true);
        Destroy(senses.enemyBody);
    }//end death
}
