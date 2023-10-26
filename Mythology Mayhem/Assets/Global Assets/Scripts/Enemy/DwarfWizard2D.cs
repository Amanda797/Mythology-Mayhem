using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfWizard2D : MonoBehaviour
{
    Enemy enemy;

    [Header("Idle & Patrol")]
    [SerializeField] string patrolBool;
    [SerializeField] float speed = .6f;
    [SerializeField] float flipSensitivity = 1f;
    [SerializeField] float patrolDistance = 5f;

    [Header("Melee Attack")]
    [SerializeField] GameObject body;
    [SerializeField] Collider2D attack;
    [SerializeField] Collider2D playerCollider;
    [SerializeField] string[] meleeAttackTriggers;
    [SerializeField] GameObject[] familiars;
    [SerializeField] float meleeDistance = 10f;
    [SerializeField] float alertTimer = 3f;
    float alertTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        attack = enemy.attackCollider.GetComponent<BoxCollider2D>();
        playerCollider = enemy.player.GetComponent<BoxCollider2D>();
    }

    public void Idle()
    {
        if (playerCollider != null)
        {
            //Check for Player
            if (attack.IsTouching(playerCollider))
            {
                StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Attack, 0));
            }
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
        if (attack.IsTouching(playerCollider))
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

    public void SummonFamiliar()
    {
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < patrolDistance && enemy.CanAttack)
        {
            GameObject summonedFamiliar;

            if (familiars.Length > 1)
            {
                summonedFamiliar = familiars[Random.Range(0, familiars.Length - 1)];
            } else
            {
                summonedFamiliar = familiars[0];
            }

            Instantiate(summonedFamiliar, this.gameObject.transform);

            StartCoroutine(enemy.AttackRate());
        }
    }//end summon familiar

}
