using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclops3D : MonoBehaviour
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

    public void MoveToTarget()
    {
        if(Vector3.Distance(enemy.gameObject.transform.position, enemy.target) < 3f)
        {
            //Close enough to Idle
            enemy.SwitchStates(Enemy.EnemyStates.Idle);  
        } else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
        }
    }//end move to target

    public void SwitchAttack()
    {
        if(Vector3.Distance(body.transform.position, enemy.player.transform.position) > patrolDistance)
        {
            enemy.SwitchStates(Enemy.EnemyStates.Patrol);
        } else if (Vector3.Distance(body.transform.position, enemy.player.transform.position) < meleeDistance)
        {
            MeleeAttack();
        } else if (Vector3.Distance(body.transform.position, enemy.player.transform.position) < rangedDistance)
        {
            RangedAttack();
        }
    }//end Switch Attack

    public void MeleeAttack()
    {
        if(Vector3.Distance(body.transform.position, enemy.player.transform.position) < meleeDistance && enemy.CanAttack)
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
            enemy.animator.SetBool(runBool, true);
        }
    }//end melee attack

    public void RangedAttack()
    {
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < rangedDistance && enemy.CanAttack)
        {
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
            enemy.animator.SetBool(runBool, true);
        }
    }//end ranged attack
}
