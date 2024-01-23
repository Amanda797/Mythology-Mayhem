using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDragon3D : MonoBehaviour
{
    Enemy enemy;

    [Header("Idle & Patrol")]
    [SerializeField] string flyBool;

    [Header("Attacks")]
    [SerializeField] GameObject body;
    [SerializeField] string projectileAttackTrigger;
    [SerializeField] float projectileAttackDistance = 12f;
    [SerializeField] string lowProjectileAttackTrigger;
    [SerializeField] float lowProjectileAttackDistance = 12f;
    [SerializeField] string blastAttackTrigger;
    [SerializeField] float blastAttackDistance = 12f;
    [SerializeField] string biteAttackTrigger;
    [SerializeField] float biteAttackDistance = 12f;
    [SerializeField] string lowBiteAttackTrigger;
    [SerializeField] float lowBiteAttackDistance = 12f;
    [SerializeField] string wingAttackTrigger;
    [SerializeField] float wingAttackDistance = 12f;

    [Header("Heal")]
    [SerializeField] string healTrigger;
    [SerializeField] float healTimer = 10f;
    private bool canHeal = true;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
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
            enemy.animator.SetBool(flyBool, true);
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }
        else
        {
            enemy.agent.isStopped = true;
            enemy.idleTimer -= Time.deltaTime;
            enemy.animator.SetBool(flyBool, false);
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
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
        }
    }//end move to target

    public void Heal()
    {
        if(canHeal)
        { 
            StartCoroutine(HealSelf());
        }
    }//heal

    IEnumerator HealSelf()
    {
        enemy.GetComponent<Health>().Heal(100);
        canHeal = false;
        yield return new WaitForSeconds(healTimer);
        canHeal = true;
    }//heal self

    public void SwitchAttack()
    {
        float randomAttack = Random.Range(0, 5);

        switch(randomAttack)
        {
            case 0:
                {
                    Attack(projectileAttackTrigger, projectileAttackDistance);
                    break;
                }
            case 1:
                {
                    Attack(lowProjectileAttackTrigger, lowProjectileAttackDistance);
                    break;
                }
            case 2:
                {
                    Attack(blastAttackTrigger, blastAttackDistance);
                    break;
                }
            case 3:
                {
                    Attack(biteAttackTrigger, biteAttackDistance);
                    break;
                }
            case 4:
                {
                    Attack(lowBiteAttackTrigger, lowBiteAttackDistance);
                    break;
                }
            case 5:
                {
                    Attack(wingAttackTrigger, wingAttackDistance);
                    break;
                }

            default:
                {
                    Attack(wingAttackTrigger, wingAttackDistance);
                    break;
                }
        }
    }//switch attack

    public void ProjectileAttack()
    {
        Attack(projectileAttackTrigger, projectileAttackDistance);
    }//end projectile attack

    public void LowProjectileAttack()
    {
        Attack(lowProjectileAttackTrigger, lowProjectileAttackDistance);
    }//end low projectile attack

    public void BlastAttack()
    {
        Attack(blastAttackTrigger, blastAttackDistance);
    }//end blast attack

    public void BiteAttack()
    {
        Attack(biteAttackTrigger, biteAttackDistance);
    }//end bite attack

    public void LowBiteAttack()
    {
        Attack(lowBiteAttackTrigger, lowBiteAttackDistance);
    }//end low bite attack

    public void WingAttack()
    {
        Attack(wingAttackTrigger, wingAttackDistance);
    }//end wing attack


    void Attack(string _attack, float _distance)
    {
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(transform.position, enemy.player.transform.position) < _distance && enemy.CanAttack)
        {
            enemy.agent.isStopped = true;

            enemy.animator.SetBool(flyBool, false);
            enemy.animator.SetTrigger(_attack);

            enemy.player.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
            if (enemy.player.GetComponent<KnockBackFeedback>())
                enemy.player.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);

            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.player.transform.position);
            enemy.animator.SetBool(flyBool, true);
            Heal();
        }
    }//end melee attack



}
