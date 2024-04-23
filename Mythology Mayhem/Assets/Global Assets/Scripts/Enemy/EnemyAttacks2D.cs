using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks2D : MonoBehaviour
{
    Enemy enemy;
    float flipSensitivity = 1f;
    [SerializeField] GameObject body;

    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();

        activeProjectilePool = new List<GameObject>();
    }    

    #region Basic Melee Attack
    
    [Header("Basic Melee Attack")]
    [Range(0, 5)]
    [SerializeField] float speed = .6f;
    [SerializeField] string attackBool;
    [SerializeField] float meleeDistance = 10f;
    [SerializeField] float alertTimer = 3f;
    float alertTime = 0f;

    public void BasicMeleeAttack()
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

            enemy.PlaySound(Enemy.Soundtype.Attack);

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
    }//end basic melee attack

    #endregion

    #region Basic Ranged Attack

    [Header("Basic Range Attack")]
    [SerializeField] List<GameObject> activeProjectilePool;
    [SerializeField] List<GameObject> inactiveProjectilePool;
    [SerializeField] Transform throwPoint;
    [SerializeField] float throwForce;
    [SerializeField] string rangedAttackTrigger;
    [SerializeField] float rangedDistance = 7f;

    public void BasicRangedAttack()
    {
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < rangedDistance && enemy.CanAttack)
        {
            enemy.animator.SetTrigger(rangedAttackTrigger);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, enemy.player.transform.position, 0, 0);
            
            //pull from inactiveprojectiles to activate a "new" projectile
            if(inactiveProjectilePool.Count > 0 && enemy.CanAttack)
            {
                GameObject newProjectile = inactiveProjectilePool[0];
                activeProjectilePool.Add(newProjectile);
                inactiveProjectilePool.Remove(newProjectile);

                newProjectile.SetActive(true);
                newProjectile.transform.rotation.SetLookRotation(newDirection);
                newProjectile.GetComponent<Rigidbody>().AddForce(throwForce * newDirection, ForceMode.Force);

                enemy.CanAttack = false;
                StartCoroutine(enemy.AttackRate());
            } 
            
            //Reset active projectiles if they've been disabled to inactive list
            foreach(GameObject go in activeProjectilePool)
            {
                if(!go.activeInHierarchy)
                {
                    inactiveProjectilePool.Add(go);
                    activeProjectilePool.Remove(go);
                }
            }
            
        }
        else
        {
            //Move
            Vector2 xOnlyTargetPosition = new Vector2(enemy.player.transform.position.x, gameObject.transform.position.y);
            enemy.rigidBody2D.MovePosition(Vector2.Lerp(gameObject.transform.position, xOnlyTargetPosition, speed * Time.deltaTime));
        }
    }//end basic ranged attack
    #endregion

    #region Feet Grab Attack

    [Header("Feet Grab Attack")]
    [SerializeField] string feetGrabAttackTrigger;
    [SerializeField] float feetGrabAttackDistance = 7f;

    #endregion

}
