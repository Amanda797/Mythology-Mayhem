using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vikings3D : MonoBehaviour
{
    Enemy enemy;

    [Header("Idle & Patrol")]
    [SerializeField] string walkBool;
    [SerializeField] string runBool;
    [SerializeField] float patrolDistance = 30f;

    [Header("Melee Attack")]
    [SerializeField] GameObject body;
    [SerializeField] string meleeAttackTrigger;
    [SerializeField] float meleeDistance = 10f;

    [Header("Taunt Attack")]
    [SerializeField] GameObject summonViking;
    [SerializeField] bool summonUsed;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }

    bool idleTransition = false; 

    public void Idle()
    {
        //Check for Player
        if (enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Attack, 0));
        }
                
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
        //Check for Player
        if (enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Attack, 0));
        }
        
        // Continue M2T
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.target) < 3f)
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

    public void MeleeAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(body.transform.position, enemy.player.transform.position) < meleeDistance && enemy.CanAttack)
        {
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(meleeAttackTrigger);
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
            enemy.agent.speed = enemy.walkSpeed;
            enemy.animator.SetBool(runBool, true);
        }
    }//end melee attack

    public void SummonAttack()
    {
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < patrolDistance && !summonUsed)
        {
            //Need some kinf of visual effect or indication that the Viking used Taunt Attack
            //enemy.animator.SetTrigger(rangedAttackTrigger);
            summonUsed = true;

            Vikings3D[] summonVikings = FindObjectsOfType<Vikings3D>();

            if(summonVikings.Length > 1)
            {
                do {
                    summonViking = summonVikings[Random.Range(0, summonVikings.Length)].body;

                    if (summonViking != this.gameObject)
                    {
                        break;
                    } else
                    {
                        summonViking = null;
                    }
                } 
                while (summonViking != this.gameObject && summonViking != null);
            }            
            
            if(summonViking != null)
            {
                summonViking.GetComponent<NavMeshAgent>().speed = summonViking.GetComponent<Enemy>().runSpeed;
                summonViking.GetComponent<Enemy>().target = gameObject.transform.position;
            }            
        }
    }//end Summon Attack
}
