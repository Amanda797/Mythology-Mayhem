using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon3D : MonoBehaviour
{
    Enemy enemy;

    [Header("Idle & Patrol")]
    [SerializeField] string flyBool = "Fly";
    [SerializeField] float patrolDistance = 30f;

    [Header("Attacks")]
    [SerializeField] GameObject body;
    [SerializeField] string blastAttackTrigger = "Blast Attack";
    [SerializeField] float blastDistance = 10f;
    [SerializeField] string lowBiteAttackTrigger = "Low Bite";
    [SerializeField] float lowBiteDistance = 4f;
    [SerializeField] string wingAttackTrigger = "Wing Attack";
    [SerializeField] float wingDistance = 8f;
    [SerializeField] string biteAttackTrigger = "Bite Attack";
    [SerializeField] float biteDistance = 3f;
    [SerializeField] string castSpellTrigger = "Cast Spell";
    [SerializeField] float castSpellDistance = 10f;
    [SerializeField] string lowProjectileAttackTrigger = "Low Projectile Attack";
    [SerializeField] float lowProjectileDistance = 2f;
    [SerializeField] string projectileAttackTrigger = "Projectile Attack";
    [SerializeField] float projectileDistance = 12f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform throwPoint;
    [SerializeField] float throwForce = 300f;
    [SerializeField] float alertTimer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        body = this.gameObject;
    }

    bool idleTransition = false;

    public void Idle()
    {
        if (enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Attack, alertTimer));
        }
        else
        // Continue Idle
        if (enemy.idleTimer <= 0 && !idleTransition)
        {
            idleTransition = true;
            enemy.agent.isStopped = false;
            enemy.animator.SetBool(flyBool, true);
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }
        else
        {
            idleTransition = false;
            enemy.agent.isStopped = true;
            enemy.idleTimer -= Time.deltaTime;
            enemy.animator.SetBool(flyBool, false);
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

    public void SwitchAttack()
    {
        //Check for Player        
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        float randomAttack = Random.Range(0, 7);

        switch (randomAttack)
        {
            case 0:
                {
                    CastSpell();
                    break;
                }
            case 1:
                {
                    BlastAttack();
                    break;
                }
            case 2:
                {
                    LowBiteAttack();
                    break;
                }
            case 3:
                {
                    WingAttack();
                    break;
                }
            case 4:
                {
                    BiteAttack();
                    break;
                }
            case 5:
                {
                    ProjectileAttack();
                    break;
                }
            case 6:
                {
                    LowProjectileAttack();
                    break;
                }
            default:
                {
                    CastSpell();
                    break;
                }
        }
    }//end Switch Attack

    public void CastSpell()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < castSpellDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(flyBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(castSpellTrigger);
            enemy.player.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
            enemy.animator.SetBool(flyBool, true);
        }
    }//end CastSpell

    public void BlastAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < blastDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(flyBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(blastAttackTrigger);
            enemy.player.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
            enemy.animator.SetBool(flyBool, true);
        }

    }//end BlastAttack


    public void LowBiteAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < lowBiteDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(flyBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(lowBiteAttackTrigger);
            enemy.player.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
            enemy.animator.SetBool(flyBool, true);
        }

    }//end LowBiteAttack


    public void WingAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < wingDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(flyBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(wingAttackTrigger);
            enemy.player.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
            enemy.animator.SetBool(flyBool, true);
        }

    }//end WingAttack


    public void BiteAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < biteDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(flyBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(biteAttackTrigger);
            enemy.player.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
            enemy.animator.SetBool(flyBool, true);
        }

    }//end BiteAttack


    public void ProjectileAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        //Check for Projectile Prefab and Throw Point Transform
        if (projectilePrefab == null && throwPoint == null)
        {
            SwitchAttack();
        }
        else

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < projectileDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(flyBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(projectileAttackTrigger);

            GameObject projectile = Instantiate(projectilePrefab, throwPoint);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, enemy.player.transform.position, 0, 0);
            projectile.transform.rotation.SetLookRotation(newDirection);
            //projectile.GetComponent<SnowballProjectile>().enemy = enemy;
            projectile.GetComponent<Rigidbody>().AddForce(throwForce * newDirection, ForceMode.Force);

            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
            enemy.animator.SetBool(flyBool, true);
        }

    }//end ProjectileAttack


    public void LowProjectileAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        //Check for Projectile Prefab and Throw Point Transform
        if (projectilePrefab == null && throwPoint == null)
        {
            SwitchAttack();
        }
        else

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < lowProjectileDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(flyBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(lowProjectileAttackTrigger);

            GameObject projectile = Instantiate(projectilePrefab, throwPoint);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, enemy.player.transform.position, 0, 0);
            projectile.transform.rotation.SetLookRotation(newDirection);
            //projectile.GetComponent<SnowballProjectile>().enemy = enemy;
            projectile.GetComponent<Rigidbody>().AddForce(throwForce * newDirection, ForceMode.Force);

            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
            enemy.animator.SetBool(flyBool, true);
        }

    }//end LowProjectileAttack
}
