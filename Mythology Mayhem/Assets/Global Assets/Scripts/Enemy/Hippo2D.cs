using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hippo2D : MonoBehaviour
{
    Enemy enemy;

    [Header("Movements")]
    [SerializeField] string walkBool = "Walking";
    [SerializeField] string runBool = "Running";
    [SerializeField] string swimBool = "InWater";
    [SerializeField] float speed = .6f;
    [SerializeField] float flipSensitivity = 1f;
    [SerializeField] float patrolDistance = 5f;

    [Header("Attacks")]
    [SerializeField] GameObject body;
    [SerializeField] string biteTrigger = "Bite";
    [SerializeField] float biteDistance = 2f;
    [SerializeField] string chargeTrigger = "Charge";
    [SerializeField] float chargeDistance = 10f;

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
            enemy.animator.SetBool(walkBool, true);
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }
        else
        {
            enemy.animator.SetBool(walkBool, false);
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
            if (enemy.target.x + flipSensitivity > gameObject.transform.position.x && gameObject.transform.rotation.y != 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            }
            else if (enemy.target.x + flipSensitivity < gameObject.transform.position.x && gameObject.transform.rotation.y != 180)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            //Move
            Vector2 xOnlyTargetPosition = new Vector2(enemy.target.x, gameObject.transform.position.y);
            enemy.rigidBody2D.MovePosition(Vector2.Lerp(gameObject.transform.position, xOnlyTargetPosition, speed * Time.deltaTime));
        }
    }//end move to target
    public void SwitchAttack()
    {
        int randomAttack = Random.Range(0, 2);

        switch (randomAttack)
        {
            case 0:
                {
                    ChargeAttack();
                    break;
                }
            case 1:
                {
                    BiteAttack();
                    break;
                }
            default:
                {
                    BiteAttack();
                    break;
                }
        }//end switch
    }//end switch attack

    public void ChargeAttack()
    {
        Attack(chargeTrigger, chargeDistance);
    }//end charge attack

    public void BiteAttack()
    {
        Attack(biteTrigger, biteDistance);
    }//end bite attack

    void Attack(string trigger, float distance)
    {
        //Check for Player
        if (enemy.DetectPlayer())
        {
            //Flip, Rotate Y
            if (enemy.target.x + flipSensitivity > gameObject.transform.position.x && gameObject.transform.rotation.y != 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            }
            else if (enemy.target.x + flipSensitivity < gameObject.transform.position.x && gameObject.transform.rotation.y != 180)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            //Act
            if (Vector3.Distance(enemy.gameObject.transform.position, enemy.target) < distance)
            {
                //Attack
                enemy.animator.SetBool(walkBool, false);
                enemy.animator.SetTrigger(trigger);
                enemy.player.GetComponent<Health>().TakeDamage(enemy.attackDamage);
                enemy.CanAttack = false;
                StartCoroutine(enemy.AttackRate());
            }
            else
            {
                //Move
                enemy.animator.SetBool(walkBool, true);
                Vector2 xOnlyTargetPosition = new Vector2(enemy.target.x, gameObject.transform.position.y);
                enemy.rigidBody2D.MovePosition(Vector2.Lerp(gameObject.transform.position, xOnlyTargetPosition, speed * Time.deltaTime));
            }
        }
        else
        {
            enemy.animator.SetBool(walkBool, false);
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Idle, 0));
        }
    }//end attack
}
