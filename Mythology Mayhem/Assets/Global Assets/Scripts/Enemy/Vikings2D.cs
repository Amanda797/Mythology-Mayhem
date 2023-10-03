using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vikings2D : MonoBehaviour
{
    Enemy enemy;

    [Header("Idle & Patrol")]
    [SerializeField] string patrollingBool;
    [SerializeField] float speed = .6f;
    [SerializeField] float flipSensitivity = 1f;
    [SerializeField] float patrolDistance = 5f;

    [Header("Melee Attack")]
    [SerializeField] GameObject body;
    [SerializeField] string meleeAttackTrigger;
    [SerializeField] float meleeDistance = 10f;

    [Header("Taunt Attack")]
    [SerializeField] GameObject summonViking;
    [SerializeField] bool summonUsed;
    [SerializeField] string tauntAttackTrigger;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }

    public void Idle()
    {
        if(enemy.idleTimer <= 0)
        {
            enemy.animator.SetBool(patrollingBool, true);
            enemy.SwitchStates(Enemy.EnemyStates.Patrol);
        } else
        {
            enemy.idleTimer -= Time.deltaTime;
            enemy.animator.SetBool(patrollingBool, false);
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

    public void MeleeAttack()
    {
        if(Vector3.Distance(body.transform.position, enemy.player.transform.position) < meleeDistance && enemy.CanAttack)
        {
            enemy.animator.SetTrigger(meleeAttackTrigger);
            enemy.player.GetComponent<PlayerStats>().TakeDamage(enemy.attackDamage);
            if (enemy.player.GetComponent<KnockBackFeedback>())
                enemy.player.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
            enemy.CanAttack = false;
            enemy.animator.SetTrigger(meleeAttackTrigger);
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

    public void SummonAttack()
    {
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < patrolDistance && !summonUsed)
        {
            //Need some kinf of visual effect or indication that the Viking used Taunt Attack
            //enemy.animator.SetTrigger(rangedAttackTrigger);
            summonUsed = true;

            Vikings2D[] summonVikings = FindObjectsOfType<Vikings2D>();

            if (summonVikings.Length > 1)
            {
                do
                {
                    summonViking = summonVikings[Random.Range(0, summonVikings.Length)].body;
                    if (summonViking != this.gameObject)
                    {
                        break;
                    }
                    else
                    {
                        summonViking = null;
                    }
                }
                while (summonViking != this.gameObject && summonViking != null);
            }

            if (summonViking != null)
            {
                if(tauntAttackTrigger != "")
                {
                    enemy.animator.SetTrigger(tauntAttackTrigger);
                }
                
                enemy.rigidBody2D.MovePosition(Vector2.Lerp(gameObject.transform.position, gameObject.transform.position, Time.deltaTime));
            }
        }
    }//end Summon Attack
}
