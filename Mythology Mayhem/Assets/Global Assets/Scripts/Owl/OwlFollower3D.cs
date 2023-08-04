using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OwlFollower3D : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private float originalSpeed;
    private float orginalAcceleration;
    private float originalAngularSpeed;
    [SerializeField] private float rubberbandMultiplier = 3f;
    private bool attacking = false;
    [SerializeField] private float attackDistance = 10f;
    [SerializeField] private float moveAttackSpeedMulitiplier = 5f;

    public enum ObjectType
    {
        Player,
        Enemy
    }
    public ObjectType objectType;
    public float damage = 10f;
    public float range = 100f;
    public Vector3 offset = new Vector3(0, 0, 0);
    public GameObject attackPoint;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player"); 
        originalSpeed = agent.speed;  
        orginalAcceleration = agent.acceleration;  
        originalAngularSpeed = agent.angularSpeed;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
            Transform closestTarget = targets[0].transform;

            foreach (GameObject target in targets)
            {
                if(Vector3.Distance(transform.position, target.transform.position) < Vector3.Distance(transform.position, closestTarget.position))
                {
                    closestTarget = target.transform;
                }
            }

            StartCoroutine(Attack(closestTarget));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attacking)
        {
            return;
        }

        agent.SetDestination(player.transform.position);

        if (agent.remainingDistance > 20f)
        {
            agent.speed = originalSpeed * rubberbandMultiplier;
            agent.acceleration = orginalAcceleration * rubberbandMultiplier;
            agent.angularSpeed = originalAngularSpeed * rubberbandMultiplier;
        }
        else
        {
            agent.speed = originalSpeed; 
            agent.acceleration = orginalAcceleration;  
            agent.angularSpeed = originalAngularSpeed;
        }
      
    }

    IEnumerator Attack(Transform target)
    {
        attacking = true;
        bool attackComplete = false;
        Debug.Log("Attacking");

        agent.velocity = Vector3.zero;
        
        while (!attackComplete)
        {
            #region Movement

            agent.SetDestination(target.position);
            agent.speed = originalSpeed * moveAttackSpeedMulitiplier;
            agent.acceleration = orginalAcceleration * moveAttackSpeedMulitiplier;
            agent.angularSpeed = originalAngularSpeed * moveAttackSpeedMulitiplier;

            if (target == null)
            {
                attackComplete = true;
            }
            #endregion

            #region Attack
            
        
            //use overlap sphere to detect enemies in range based on the offset and the attackPoint

            Vector3 attackPointPos = attackPoint.transform.TransformPoint(offset);
            Collider[] hitEnemies = Physics.OverlapSphere(attackPointPos, range);

            // Collider[] hitEnemies = Physics.OverlapSphere(transform.position + offset, range);
            foreach (Collider enemy in hitEnemies)
            {
                if (objectType == ObjectType.Player)
                {
                    if (enemy.gameObject.tag == "Enemy")
                    {
                        Health health = enemy.GetComponent<Health>();
                        if (health != null)
                        {
                            if (health.GetHealth() > 0)
                            {
                                health.TakeDamage(damage);
                            }
                            else
                            {
                                health.Death();
                            }
        
                            attackComplete = true;
                        }
                    }
                    else if (enemy.gameObject.tag == "Medusa")
                    {
                        MedusaControlScript mcs = enemy.gameObject.GetComponent<MedusaControlScript>();
                        if (mcs != null)
                        {
                            mcs.MedusaDamage(damage);
                            attackComplete= true;
                        }
                    }
                }
                else if (objectType == ObjectType.Enemy)
                {
                    print(enemy.gameObject.name);
                    if (enemy.gameObject.tag == "Player")
                    {
                        print("We hit " + enemy.name);
                        Health health = enemy.GetComponent<Health>();
                        enemySimpleAI enemyAI = enemy.GetComponent<enemySimpleAI>();
                        if (health != null)
                        {
                            health.TakeDamage(damage);
                            if (health.GetHealth() > 0)
                            {
                                enemyAI.Hurt();
                            }
                            else
                            {
                                //enemyAI.Die();
                                health.Death();
                            }
                            attackComplete = true;
                        }
                    }
                }
            }
        
            #endregion
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.5f);
        attacking = false;
        Debug.Log("End Attacking");
        yield return null;
    }



}
