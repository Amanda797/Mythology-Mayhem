using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearWarrior3D : MonoBehaviour
{
    Enemy enemy;

    [Header("Idle & Patrol")]
    [SerializeField] string walkBool;
    [SerializeField] string runBool;
    [SerializeField] float patrolDistance = 30f;
    [SerializeField] GameObject body;
    [SerializeField] float alertTimer = 3f;

    [Header("Slash Attack")]
    [SerializeField] string slashTrigger;
    [SerializeField] float slashDistance = 17f;

    [Header("Chop Attack")]
    [SerializeField] string chopTrigger;
    [SerializeField] float chopDistance = 10f;

    [Header("Claw Attack")]
    [SerializeField] string clawTrigger;
    [SerializeField] float clawDistance = 14f;

    [Header("Breath Attack")]
    [SerializeField] string breathTrigger;
    [SerializeField] float breathDistance = 20f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        enemy.animator.SetBool(walkBool, false);
        enemy.animator.SetBool(runBool, false);
    }

    bool idleTransition = false;

    public void Idle()
    {
        transform.LookAt(enemy.agent.steeringTarget);

        if (enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Attack, alertTimer));
        }
        else
        // Continue Idle
        if (enemy.idleTimer <= 0 && !idleTransition)
        {
            idleTransition = true;
            enemy.agent.isStopped = false;
            enemy.animator.SetBool(walkBool, true);
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }
        else
        {
            idleTransition = false;
            enemy.agent.isStopped = true;
            enemy.idleTimer -= Time.deltaTime;
            enemy.animator.SetBool(walkBool, false);
        }
    }//end move to target

    public void MoveToTarget()
    {
        transform.LookAt(enemy.agent.steeringTarget);

        //Check for Player        
        if (enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Attack, 0));
        }
        else
        // Continue M2T
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.target) < patrolDistance)
        {
            //Close enough to Idle
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Idle, 0));
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
        }
    }//end move to target

    public void SwitchAttack()
    {
        //Check for Player        
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        int randomAttack = (int)Random.Range(0, 3);

        switch(randomAttack)
        {
            case 0:
                {
                    Attack(slashDistance, slashTrigger);
                    break;
                }
            case 1:
                {
                    Attack(chopDistance, chopTrigger);
                    break;
                }
            case 2:
                {
                    Attack(clawDistance, clawTrigger);
                    break;
                }
            case 3:
                {
                    Attack(breathDistance, breathTrigger);
                    break;
                }
            default:
                {
                    Attack(slashDistance, slashTrigger);
                    break;
                }
        }

    }//end Switch Attack

    public void SlashAttack()
    {
        Attack(slashDistance, slashTrigger);
    }//end slash attack

    public void ChopAttack()
    {
        Attack(chopDistance, chopTrigger);
    }//end chop attack

    public void ClawAttack()
    {
        Attack(clawDistance, clawTrigger);
    }//end claw attack

    public void BreathAttack()
    {
        Attack(breathDistance, breathTrigger);
    }//end breath attack

    public void Attack(float attackDistance, string attackTrigger)
    {
        //Check for Player       
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(body.transform.position, enemy.player.transform.position) < attackDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(runBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(attackTrigger);
            enemy.player.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
            if (enemy.player.GetComponent<KnockBackFeedback>())
                enemy.player.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.player.transform.position);
            enemy.animator.SetBool(runBool, true);
        }
    }//end attack


}
