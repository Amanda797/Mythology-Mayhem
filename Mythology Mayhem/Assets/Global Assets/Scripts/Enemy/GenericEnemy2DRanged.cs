using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy2DRanged : MonoBehaviour
{
Enemy enemy;
    public GameObject projectile;
    public Transform LaunchOffset;

    [Header("Idle & Patrol")]
    [SerializeField] string patrolBool;
    [SerializeField] float speed = .6f;
    [SerializeField] float flipSensitivity = 1f;
    [SerializeField] float patrolDistance = 5f;

    [Header("Melee Attack")]
    [SerializeField] GameObject body;
    [SerializeField] string[] meleeAttackTriggers;
    [SerializeField] string[] throwAttackTriggers;
    [SerializeField] float meleeDistance = 10f;
    [SerializeField] float alertTimer = 3f;
    float alertTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }

    public void Idle()
    {
        //Check for Player
        if (enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Attack, 0));
        }

        // Continue Idle
        if (enemy.idleTimer <= 0)
        {
            enemy.animator.SetBool(patrolBool, true);
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }
        else
        {
            enemy.animator.SetBool(patrolBool, false);
            enemy.idleTimer -= Time.deltaTime;
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
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.target) < patrolDistance)
        {
            //Close enough to Idle
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Idle, 0));
        }
        else
        {
            //Flip, Rotate Y
            if (enemy.target.x + flipSensitivity > gameObject.transform.position.x && gameObject.transform.rotation.y != 180)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

            }
            else if (enemy.target.x + flipSensitivity < gameObject.transform.position.x && gameObject.transform.rotation.y != 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            //Move
            Vector2 xOnlyTargetPosition = new Vector2(enemy.target.x, gameObject.transform.position.y);
            enemy.rigidBody2D.MovePosition(Vector2.Lerp(gameObject.transform.position, xOnlyTargetPosition, speed * Time.deltaTime));
        }
    }//end move to target

    public void MeleeAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            if (alertTime > alertTimer)
            {
                StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, alertTimer));
            }
            else
            {
                alertTime += Time.deltaTime;
            }
        }

        // Continue Attack
        if (Vector3.Distance(body.transform.position, enemy.player.transform.position) < meleeDistance && enemy.CanAttack)
        {
            enemy.animator.SetTrigger(meleeAttackTriggers[Random.Range(0,meleeAttackTriggers.Length)]);
            enemy.player.GetComponent<PlayerStats>().TakeDamage(enemy.attackDamage);
            if (enemy.player.GetComponent<KnockBackFeedback>())
                enemy.player.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
            enemy.CanAttack = false;
            enemy.animator.SetTrigger(meleeAttackTriggers[Random.Range(0, meleeAttackTriggers.Length)]);
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            //Flip, Rotate Y
            if (enemy.player.transform.position.x + flipSensitivity > gameObject.transform.position.x && gameObject.transform.rotation.y != 180)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

            }
            else if (enemy.player.transform.position.x + flipSensitivity < gameObject.transform.position.x && gameObject.transform.rotation.y != 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            //Move
            Vector2 xOnlyTargetPosition = new Vector2(enemy.player.transform.position.x, gameObject.transform.position.y);
            enemy.rigidBody2D.MovePosition(Vector2.Lerp(gameObject.transform.position, xOnlyTargetPosition, speed * Time.deltaTime));
        }
    }//end melee attack

    public void ThrowAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            if (alertTime > alertTimer)
            {
                StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, alertTimer));
            }
            else
            {
                alertTime += Time.deltaTime;
            }
        }

        // Continue Throw
        if (Vector3.Distance(body.transform.position, enemy.player.transform.position) > meleeDistance && enemy.CanAttack)
        {
            enemy.CanAttack = false;
            Instantiate(projectile, LaunchOffset.position, transform.rotation).SetActive(true);
            enemy.animator.SetTrigger(throwAttackTriggers[Random.Range(0,throwAttackTriggers.Length)]);
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            //Flip, Rotate Y
            if (enemy.player.transform.position.x + flipSensitivity > gameObject.transform.position.x && gameObject.transform.rotation.y != 180)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

            }
            else if (enemy.player.transform.position.x + flipSensitivity < gameObject.transform.position.x && gameObject.transform.rotation.y != 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            //Move
            Vector2 xOnlyTargetPosition = new Vector2(enemy.player.transform.position.x, gameObject.transform.position.y);
            enemy.rigidBody2D.MovePosition(Vector2.Lerp(gameObject.transform.position, xOnlyTargetPosition, speed * Time.deltaTime));
        }
    }//end throw attack
}
