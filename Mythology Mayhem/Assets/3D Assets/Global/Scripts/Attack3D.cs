using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attack3D : MythologyMayhem
{
    public enum ObjectType
    {
        Player,
        Enemy
    }

    public ObjectType objectType;
    public float damage = 10f;
    public float range = 100f;
    public Vector3 offset = new Vector3(0, 0, 0);
    bool isAttacking = false;
    public GameObject attackPoint;
    [HideInInspector] public int hitCount;


    void Update()
    {
        if(isAttacking)
        {
            //use overlap sphere to detect enemies in range based on the offset and the attackPoint
            Vector3 attackPointPos = attackPoint.transform.TransformPoint(offset);
            Collider[] hitEnemies = Physics.OverlapSphere(attackPointPos, range);

            foreach (Collider enemy in hitEnemies)
            {
                if (objectType == ObjectType.Player)
                {
                    if (enemy.gameObject.tag == "Enemy")
                    {
                        Health health = enemy.GetComponent<Health>();
                        if (health != null) health.TakeDamage(damage);
                    }
                    else if (enemy.gameObject.tag == "Medusa")
                    {
                        MedusaControlScript mcs = enemy.gameObject.GetComponent<MedusaControlScript>();
                        if (mcs != null) mcs.MedusaDamage(damage);
                    }
                    else if (enemy.gameObject.tag == "Tentacle") 
                    {
                        KrakenTentacleScript kts = enemy.gameObject.transform.root.GetComponent<KrakenTentacleScript>();
                        if(kts != null) kts.Damage((int)damage);
                    }
                    isAttacking = false;
                    hitCount++;
                }
                else if (objectType == ObjectType.Enemy)
                {
                    if (enemy.gameObject.tag == "Player")
                    {
                        Health health = enemy.GetComponent<Health>();
                        enemySimpleAI enemyAI = enemy.GetComponent<enemySimpleAI>();
                        if (health != null)
                        {
                            health.TakeDamage(damage);
                            if (health.GetHealth() > 0) enemyAI.Hurt();
                            else health.Death();
                            isAttacking = false;
                            hitCount++;
                        }
                    }
                }
            }
        }
    }

    public void Attack()
    {
        if (Time.timeScale == 1) isAttacking = true;
    }
    public void StopAttack()
    {
        isAttacking = false;
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }    
}
