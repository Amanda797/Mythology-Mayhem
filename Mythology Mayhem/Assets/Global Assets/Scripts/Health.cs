using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MythologyMayhem;
using static UnityEditorInternal.VersionControl.ListControl;

public class Health : MonoBehaviour
{
    // --------------------------
    // ***PROPERTIES***
    // --------------------------
    [SerializeField] private float MaxHealth;
    [SerializeField] private float _health;
    [SerializeField] private GameObject mainObject; // Parent Self
    public GameObject rewardObject; // Reward Object

    [Header("Animation")]
    [SerializeField] private Animator anim;
    [SerializeField] private string hurtTrigger;
    [SerializeField] private string deathTrigger;
    [SerializeField] private string healTrigger;

    public Enemy enemy;

    public bool _attacked = false;
    public bool _defenseUp = false;
    public float _defenseTimer = 0f;

    public float respawnTimer = 50f;
    public bool canRespawn = false;

    public EnemyLoadSystem loadSystem;
    public bool isDead = false;

    // --------------------------
    // ***METHODS***
    // --------------------------
    void Start()
    {
        enemy = this.gameObject.GetComponent<Enemy>();
        Life = MaxHealth;
    }// end start

    public float Life
    {
        get { return _health; }
        set { _health = value; }
    }

    public void SetHealth(float h) {
        Life = h;
    }// end set health

    public float GetHealth() {
        return Life;
    }//end get health

    public void Respawn(float _respawnHealth)
    {
        SetHealth(_respawnHealth);
    }//end respawn

    public void TakeDamage(float d)
    {
        //Defense Bool. If not _attacked, take damage. If _attacked, do not take damage. Use for timed, temporary defenses in specific enemies (See Boar3D)
        if (!_attacked && !_defenseUp && !isDead)
        {
            if (gameObject.tag == "Enemy")
            {
                if (enemy != null) enemy.PlaySound(Enemy.Soundtype.Hurt);
                else Debug.LogWarning("enemy is null");

                if (anim != null) anim.SetTrigger(hurtTrigger);
                else Debug.LogWarning("anim is null");

                StartCoroutine(Attacked());
            }

            Life -= d;
            Debug.Log(Life);
            if (Life <= 0) Death();
        }        
    }//end take damage

    IEnumerator Attacked()
    {
        _attacked = true;
        yield return new WaitForSeconds(_defenseTimer);
        _attacked = false;
    }

    public void Heal(float h)
    {
        if (anim != null) anim.SetTrigger(healTrigger);

        Life += Mathf.Clamp(h,h,MaxHealth);
    }

    public void Death()
    {
        if (!isDead)
        {
            Debug.Log("Death1");
            isDead = true;

            if (loadSystem != null) loadSystem.SyncToSave(this.gameObject.transform.parent.gameObject);

            if (enemy != null) enemy.PlaySound(Enemy.Soundtype.Death);

            if (anim != null && deathTrigger != "") anim.SetTrigger(deathTrigger);

            if (gameObject.tag == "Enemy")
            {
                //Make rigidbody static
                if (gameObject.GetComponent<Enemy>() && gameObject.GetComponent<Enemy>().enemyDimension == MythologyMayhem.Dimension.TwoD)
                {
                    Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    rb.velocity = new Vector2(0, 0);
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                else if (gameObject.GetComponent<Enemy>() && gameObject.GetComponent<Enemy>().enemyDimension == MythologyMayhem.Dimension.ThreeD)
                {
                    Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                    rb.isKinematic = true;
                    rb.velocity = new Vector2(0, 0);
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                }

                if (enemy.hasDeadDelegate) enemy.DeadDelegate.Invoke();
            }

            StartCoroutine(DeathTimer(2f));
        }
    }

    public IEnumerator DeathTimer(float time)
    {
        yield return new WaitForSeconds(time);

        if(rewardObject != null)
        {
            GameObject reward = Instantiate(rewardObject, transform.position + Vector3.up*3, transform.rotation);
            reward.name = rewardObject.name;
        }

        if(mainObject.CompareTag("Companion")) StartCoroutine(CompanionRespawn(respawnTimer));
        else
        {
            enemy._localGameManager.enemies.Remove(mainObject);
            Destroy(mainObject);
        }
    }//end death timer

     public IEnumerator CompanionRespawn(float time)
    {
        canRespawn = false;
        yield return new WaitForSeconds(time);
        canRespawn = true;
    }//end companion respawn timer

}//end health class
