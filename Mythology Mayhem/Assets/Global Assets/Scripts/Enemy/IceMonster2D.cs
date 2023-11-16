using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMonster2D : MonoBehaviour { 

    Enemy enemy;

    [Header("Idle & Patrol")]
    [Range(0, 5)]
    [SerializeField] float speed = .6f;
    [SerializeField] float flipSensitivity = 1f;
    [SerializeField] float patrolDistance = 10f;

    [Header("Melee Attack")]
    [SerializeField] GameObject body;
    [SerializeField] Collider2D attack;
    [SerializeField] Collider2D playerCollider;
    [SerializeField] string meleeAttackTrigger;
    [SerializeField] float meleeDistance = .5f;
    [SerializeField] float alertTimer = 3f;
    float alertTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        attack = enemy.attackCollider.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (playerCollider == null && enemy.player.GetComponent<BoxCollider2D>())
        {
            playerCollider = enemy.player.GetComponent<BoxCollider2D>();
        }
    }

    public void Idle()
    {
        if (playerCollider != null)
        {
            //Check for Player
            if (enemy.triggerDetector2D.triggered && enemy.triggerDetector2D.otherCollider2D.CompareTag("Player"))
            {
                StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Attack, 0));
            }
        }

        // Continue Idle
        if (enemy.idleTimer <= 0)
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }
        else
        {
            enemy.idleTimer -= Time.deltaTime;
        }
    }

    public void MoveToTarget()
    {
        //Check for Player
        if (enemy.triggerDetector2D.triggered && enemy.triggerDetector2D.otherCollider2D.CompareTag("Player"))
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
    }

    public void MeleeAttack()
    {
        //Check for Player
        if (!enemy.triggerDetector2D.triggered)
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
            enemy.rigidBody2D.MovePosition(Vector2.Lerp(gameObject.transform.position, enemy.player.transform.position, (speed * 3f) * Time.deltaTime));
        }
    }

}
