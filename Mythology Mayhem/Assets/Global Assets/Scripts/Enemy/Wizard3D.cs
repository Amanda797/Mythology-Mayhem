using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard3D : MonoBehaviour
{
    Enemy enemy;

    [Header("Idle & Patrol")]
    [SerializeField] string patrolBool;
    [SerializeField] float patrolDistance = 30f;

    [Header("Melee Attack")]
    [SerializeField] GameObject body;
    [SerializeField] string[] meleeAttackTriggers;
    [SerializeField] GameObject[] familiars;
    [SerializeField] float meleeDistance = 15f;

    [Header("Range Attack")]
    [SerializeField] GameObject snowballPrefab;
    [SerializeField] Transform throwPoint;
    [SerializeField] float throwForce;
    [SerializeField] string rangedAttackTrigger;
    [SerializeField] float rangedDistance = 20f;

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
        else
        // Continue Idle
        if (enemy.idleTimer <= 0 && !idleTransition)
        {
            idleTransition = true;
            enemy.agent.isStopped = false;
            enemy.animator.SetBool(patrolBool, true);
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol,0));
        } else
        {
            idleTransition = false;
            enemy.agent.isStopped = true;
            enemy.idleTimer -= Time.deltaTime;
            enemy.animator.SetBool(patrolBool, false);
        }
    }//end move to target

    public void MoveToTarget()
    {
        //Check for Player
        if (enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Attack, 0));
        }
        else
        // Continue M2T
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.target) < 3f)
        {
            //Close enough to Idle
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Idle, 0));
        } else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
        }
    }//end move to target

    public void RangedAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < rangedDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(patrolBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(rangedAttackTrigger);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, enemy.player.transform.position, 0, 0);
            GameObject snowball = Instantiate(snowballPrefab, throwPoint.position, throwPoint.rotation);
            snowball.transform.rotation.SetLookRotation(newDirection);
            snowball.GetComponent<SnowballProjectile>().enemy = enemy;
            snowball.GetComponent<Rigidbody>().AddForce(throwForce * newDirection, ForceMode.Force);
            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.player.transform.position);
            enemy.animator.SetBool(patrolBool, true);
        }
    }//end ranged attack

    public void SummonFamiliar()
    {
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < meleeDistance && enemy.CanAttack)
        {
            GameObject summonedFamiliar;

            if (familiars.Length > 1)
            {
                summonedFamiliar = familiars[Random.Range(0, familiars.Length)];
            }
            else
            {
                summonedFamiliar = familiars[0];
            }

            Instantiate(summonedFamiliar, new Vector3((gameObject.transform.position.x + enemy.player.transform.position.x) / 2, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);

            StartCoroutine(enemy.AttackRate());
        }
    }//end summon familiar
}
