using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar3D : MonoBehaviour
{
    Enemy enemy;

    [Header("Idle & Patrol")]
    [SerializeField] string walkBool;
    [SerializeField] string runBool;

    [Header("Melee Attack")]
    [SerializeField] GameObject body;
    [SerializeField] string[] attackTriggers;
    [SerializeField] float meleeDistance = 2f;

    [Header("Defense")]
    [SerializeField] string defenseBool;
    [SerializeField] float defenseTimer = 2f;
    float defenseTime;

    // Start is called before the first frame update
    void Start()
    {
        //Ignore Enemy and 3DGround Collisions
        Physics.IgnoreLayerCollision(6, 9);

        enemy = gameObject.GetComponent<Enemy>();
        GetComponent<Health>()._defenseTimer = defenseTimer;
    }

    public void Idle()
    {
        transform.LookAt(enemy.agent.steeringTarget);

        if (enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Attack, 0));
        }
        else
        // Continue Idle
        if (enemy.idleTimer <= 0 && enemy.currentStatePosition == Enemy.StatePosition.Current)
        {
            enemy.agent.isStopped = false;
            enemy.animator.SetBool(walkBool, true);
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol,0));
        } else
        {
            enemy.agent.isStopped = true;
            enemy.idleTimer -= Time.deltaTime;
            enemy.animator.SetBool(walkBool, false);
        }
    }//end move to target

    public void MoveToTarget()
    {
        transform.LookAt(enemy.agent.steeringTarget);

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

    public void DetermineAction()
    {
        Debug.Log("DetermineAction");
        transform.LookAt(enemy.agent.steeringTarget);

        if (enemy.health._attacked == true)
        {
            Defend();
        } else
        {
            MeleeAttack();
        }
    }

    public void MeleeAttack()
    {
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        //Debug.Log("Self: " + body.transform.position + " " + body.name + " < Player: " + enemy.player.transform.position + " = " + (Vector3.Distance(body.transform.position, enemy.player.transform.position) < meleeDistance));

        // Continue Attack
        if (Vector3.Distance(transform.position, enemy.player.transform.position) < meleeDistance && enemy.CanAttack)
        {
            enemy.agent.isStopped = true;

            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, false);
            enemy.animator.SetTrigger(attackTriggers[Random.Range(0,attackTriggers.Length)]);

            enemy.player.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
            if (enemy.player.GetComponent<KnockBackFeedback>())
                enemy.player.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);

            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.player.transform.position);
            enemy.animator.SetBool(runBool, true);
        }
    }//end melee attack

    public void Defend()
    {
        //if (defenseTime >= defenseTimer)
        //{
        //    enemy.animator.SetBool(defenseBool, false);
        //    defenseTime = 0;
        //    enemy.health._defenseUp = false;
        //    enemy.health._attacked = true;
        //}
        //else
        //{
        //    enemy.animator.SetBool(defenseBool, true);
        //    defenseTime += Time.deltaTime;
        //    enemy.health._defenseUp = true;
        //}
        if (!enemy.health._defenseUp) StartCoroutine(ToggleDefend());
    }
    IEnumerator ToggleDefend()
    {
        enemy.animator.SetBool(defenseBool, true);
        enemy.health._defenseUp = true;
        yield return new WaitForSeconds(defenseTimer);
        enemy.animator.SetBool(defenseBool, false);
        enemy.health._defenseUp = false;
    }
}
