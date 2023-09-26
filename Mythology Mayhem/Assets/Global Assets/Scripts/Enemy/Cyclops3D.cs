using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclops3D : MonoBehaviour
{
    Enemy enemy;
    GameObject player;

    [Header("Idle & Patrol")]
    [SerializeField] string walkBool;
    [SerializeField] string runBool;
    [SerializeField] float patrolDistance = 30f;

    [Header("Melee Attack")]
    [SerializeField] GameObject body;
    [SerializeField] string meleeAttackTrigger;
    [SerializeField] float meleeDistance = 10f;

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

    public void Idle()
    {
        if(enemy.idleTimer <= 0)
        {
            enemy.agent.isStopped = false;
            enemy.animator.SetBool(walkBool, true);
            enemy.SwitchStates(Enemy.EnemyStates.Patrol);
        } else
        {
            enemy.agent.isStopped = true;
            enemy.idleTimer -= Time.deltaTime;
            enemy.animator.SetBool(walkBool, false);
        }
    }//end move to target

    public void MoveToTarget(Vector3 targetPosition)
    {
        if(Vector3.Distance(enemy.gameObject.transform.position, targetPosition) < 3f)
        {
            //Close enough to Idle
            enemy.SwitchStates(Enemy.EnemyStates.Idle);  
        } else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(targetPosition);
        }
    }//end move to target

    public void SwitchAttack(GameObject player)
    {
        if(Vector3.Distance(body.transform.position, player.transform.position) > patrolDistance)
        {
            enemy.SwitchStates(Enemy.EnemyStates.Patrol);
        } else if (Vector3.Distance(body.transform.position, player.transform.position) < meleeDistance)
        {
            MeleeAttack(player);
        } else if (Vector3.Distance(body.transform.position, player.transform.position) < rangedDistance)
        {
            RangedAttack(player);
        }
    }//end Switch Attack

    public void MeleeAttack(GameObject player)
    {
        if(Vector3.Distance(body.transform.position, player.transform.position) < meleeDistance && enemy.CanAttack)
        {
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(meleeAttackTrigger);
            player.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
            if (player.GetComponent<KnockBackFeedback>())
                player.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(player.transform.position);
            enemy.animator.SetBool(runBool, true);
        }
    }//end melee attack

    public void RangedAttack(GameObject player)
    {
        if (Vector3.Distance(enemy.gameObject.transform.position, player.transform.position) < rangedDistance && enemy.CanAttack)
        {
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(rangedAttackTrigger);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, player.transform.position, 0, 0);
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
            enemy.agent.SetDestination(player.transform.position);
            enemy.animator.SetBool(runBool, true);
        }
    }//end ranged attack
}
