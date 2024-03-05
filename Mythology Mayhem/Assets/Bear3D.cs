using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear3D : MonoBehaviour
{
    Enemy enemy;

    [Header("Idle & Patrol")]
    [SerializeField] string walkBool = "Walk Forward";
    [SerializeField] string runBool = "Run Forward";
    [SerializeField] float patrolDistance = 30f;

    [Header("Attacks")]
    [SerializeField] GameObject body;
    [SerializeField] string castSpellTrigger = "Cast Spell";
    [SerializeField] float castSpellDistance = 10f;
    [SerializeField] string slashAttackTrigger = "Slash Attack";
    [SerializeField] float slashDistance = 4f;
    [SerializeField] string jumpAttackTrigger = "Jump Attack";
    [SerializeField] float jumpDistance = 8f;
    [SerializeField] string chopAttackTrigger = "Chop Attack";
    [SerializeField] float chopDistance = 3f;
    [SerializeField] string clawAttackTrigger = "Claw Attack";
    [SerializeField] float clawDistance = 2f;
    [SerializeField] string projectileAttackTrigger = "Projectile Attack";
    [SerializeField] float projectileDistance = 12f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform throwPoint;
    [SerializeField] float throwForce = 300f;
    [SerializeField] string breathAttackTrigger = "Breath Attack";
    [SerializeField] float breathDistance = 5f;
    [SerializeField] string defendAttackBool = "Defend";
    [SerializeField] float alertTimer = 3f;

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
            enemy.animator.SetBool(walkBool, true);
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }
        else
        {
            idleTransition = false;
            enemy.agent.isStopped = true;
            enemy.idleTimer -= Time.deltaTime;
            enemy.animator.SetBool(walkBool, false);
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

        switch(randomAttack)
        {
            case 0:
                {
                    CastSpell();
                    break;
                }
            case 1:
                {
                    SlashAttack();
                    break;
                }
            case 2:
                {
                    JumpAttack();
                    break;
                }
            case 3:
                {
                    ChopAttack();
                    break;
                }
            case 4:
                {
                    ClawAttack();
                    break;
                }
            case 5:
                {
                    ProjectileAttack();
                    break;
                }
            case 6:
                {
                    BreathAttack();
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
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, false);
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
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, true);
        }
    }//end CastSpell

    public void SlashAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < castSpellDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, false);
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
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, true);
        }

    }//end SlashAttack


    public void JumpAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < jumpDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(jumpAttackTrigger);
            enemy.player.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, true);
        }

    }//end JumpAttack


    public void ChopAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < chopDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(chopAttackTrigger);
            enemy.player.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, true);
        }

    }//end ChopAttack


    public void ClawAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < clawDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(clawAttackTrigger);
            enemy.player.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, true);
        }

    }//end ClawAttack


    public void ProjectileAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        //Check for Projectile Prefab and Throw Point Transform
        if(projectilePrefab == null && throwPoint == null)
        {
            SwitchAttack();
        } else

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < projectileDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, false);
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
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, true);
        }

    }//end ProjectileAttack


    public void BreathAttack()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (Vector3.Distance(enemy.gameObject.transform.position, enemy.player.transform.position) < breathDistance && enemy.CanAttack)
        {
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetTrigger(breathAttackTrigger);
            enemy.player.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
            enemy.CanAttack = false;
            StartCoroutine(enemy.AttackRate());
        }
        else
        {
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target);
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, true);
        }

    }//end BreathAttack

    public void Defend()
    {
        //Check for Player
        if (!enemy.DetectPlayer())
        {
            StartCoroutine(enemy.SwitchStates(Enemy.EnemyStates.Patrol, 0));
        }

        // Continue Attack
        if (enemy.health.canDefend && enemy.health.attacked)
        {
            enemy.animator.SetBool(walkBool, false);
            enemy.animator.SetBool(runBool, false);
            enemy.agent.isStopped = true;
            enemy.animator.SetBool(defendAttackBool, true);
        } else
        {
            enemy.animator.SetBool(defendAttackBool, false);
            enemy.agent.isStopped = false;
        }

    }//end Defend
}
