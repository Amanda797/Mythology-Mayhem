using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclops2D : MonoBehaviour
{
    Enemy enemy;

    [Header("Idle & Patrol")]
    [SerializeField] string patrolBool;
    [Range(0, 5)]
    [SerializeField] float speed = .6f;
    float flipSensitivity = 1f;
    [SerializeField] float patrolDistance = 10f;

    [Header("Melee Attack")]
    [SerializeField] GameObject body;
    [SerializeField] string attackBool;
    [SerializeField] float meleeDistance = 4f;

    [Header("Range Attack")]
    [SerializeField] GameObject snowballPrefab;
    [SerializeField] Transform throwPoint;
    [SerializeField] float throwForce;
    [SerializeField] string rangedAttackTrigger;
    [SerializeField] float rangedDistance = 7f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }

    public void Idle()
    {
        if (enemy.idleTimer <= 0)
        {
            enemy.animator.SetBool(patrolBool, true);
            enemy.SwitchStates(Enemy.EnemyStates.Patrol);
        }
        else
        {
            enemy.animator.SetBool(patrolBool, false);
            enemy.idleTimer -= Time.deltaTime;
        }
    }//end move to target

    public void MoveToTarget()
    {
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.target) < patrolDistance)
        {
            //Close enough to Idle
            enemy.SwitchStates(Enemy.EnemyStates.Idle);
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
        if (Vector3.Distance(body.transform.position, enemy.player.transform.position) < meleeDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(attackBool, true);
            enemy.player.GetComponent<PlayerStats>().TakeDamage(enemy.attackDamage);
            if (enemy.player.GetComponent<KnockBackFeedback>())
                enemy.player.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
            enemy.CanAttack = false;
            enemy.animator.SetBool(attackBool, false);
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

    public void RangedAttack()
    {
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < rangedDistance && enemy.CanAttack)
        {
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
            //Move
            Vector2 xOnlyTargetPosition = new Vector2(enemy.player.transform.position.x, gameObject.transform.position.y);
            enemy.rigidBody2D.MovePosition(Vector2.Lerp(gameObject.transform.position, xOnlyTargetPosition, speed * Time.deltaTime));
        }
    }//end ranged attack
}
