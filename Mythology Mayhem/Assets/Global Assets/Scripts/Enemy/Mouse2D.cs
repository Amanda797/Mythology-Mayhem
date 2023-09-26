using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse2D : MonoBehaviour
{
    Enemy enemy;
    GameObject player;

    [Header("Idle & Patrol")]
    [SerializeField] string patrolBool;
    [SerializeField] float speed = .6f;
    [SerializeField] float flipSensitivity = 1f;

    [Header("Melee Attack")]
    [SerializeField] GameObject body;
    [SerializeField] string attackBool;
    [SerializeField] float meleeDistance = 10f;

    [Header("Range Attack")]
    [SerializeField] bool dropsScrolls;
    [SerializeField] float countdown = 4f;
    [SerializeField] GameObject fallingScroll;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }

    public void Idle()
    {
        if(enemy.idleTimer <= 0)
        {
            enemy.animator.SetBool(patrolBool, true);
            enemy.SwitchStates(Enemy.EnemyStates.Patrol);
        } else
        {
            enemy.animator.SetBool(patrolBool, false);
            enemy.idleTimer -= Time.deltaTime;
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
            //Flip, Rotate Y
            if(targetPosition.x + flipSensitivity > gameObject.transform.position.x && gameObject.transform.rotation.y != 180)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

            } else if (targetPosition.x + flipSensitivity < gameObject.transform.position.x && gameObject.transform.rotation.y != 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            //Move
            enemy.rigidBody2D.MovePosition(Vector2.Lerp(gameObject.transform.position, targetPosition, speed));
        }
    }//end move to target

    public void MeleeAttack(GameObject player)
    {
        if(Vector3.Distance(body.transform.position, player.transform.position) < meleeDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(attackBool, true);
            player.GetComponent<PlayerStats>().TakeDamage(enemy.attackDamage);
            if (player.GetComponent<KnockBackFeedback>())
                player.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
            enemy.CanAttack = false;
            enemy.animator.SetBool(attackBool, false);
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.rigidBody2D.MovePosition(Vector2.Lerp(gameObject.transform.position, player.transform.position, speed));
        }
    }//end melee attack

    public void DropScrolls(Vector3 nullPosition)
    {
        if (dropsScrolls)
        {
            if (countdown < 0)
            {
                countdown = Random.Range(3, countdown);
                //spawn fallingscroll prefab
                Instantiate(fallingScroll, transform.position, Quaternion.identity);
            }
            else
            {
                countdown -= 1 * Time.deltaTime;
            }
        }
    }//end ranged attack
}
