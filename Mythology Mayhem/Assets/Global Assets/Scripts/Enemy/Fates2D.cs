using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fates2D : MonoBehaviour
{
    Enemy enemy;

    [Header("Idle & Patrol")]
    [SerializeField] string patrolBool;
    [Range(0,5)]
    [SerializeField] float speed = .6f;
    float flipSensitivity = 1f;

    [Header("Melee Attack")]
    [SerializeField] GameObject body;
    [SerializeField] string attackBool;
    [SerializeField] string attackLock;
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
        //No Idle
    }//end move to target

    public void MoveToTarget()
    {
        //No Move to Target
    }//end move to target

    public void MeleeAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            if (alertTime > alertTimer)
            {
                StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Attack, alertTimer));
            }
            else
            {
                alertTime += Time.deltaTime;
            }
        }

        // Continue Attack
        if (Vector3.Distance(body.transform.position, enemy.player.transform.position) < meleeDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(attackBool, true);
            enemy.player.GetComponent<PlayerStats>().TakeDamage(enemy.attackDamage);
            if (enemy.player.GetComponent<KnockBackFeedback>())
                enemy.player.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
            enemy.CanAttack = false;
            enemy.animator.SetBool(attackBool, false);
            enemy.animator.SetBool(attackLock, false);
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
            if(enemy.rigidBody2D.bodyType != RigidbodyType2D.Static)
            {
                enemy.rigidBody2D.MovePosition(Vector2.Lerp(gameObject.transform.position, xOnlyTargetPosition, speed * Time.deltaTime));
            }            
        }
    }//end melee attack

    public void SpecialAttack(Vector3 nullPosition)
    {
    }//end special attack
}
