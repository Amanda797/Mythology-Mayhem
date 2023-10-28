using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class WolfCompanion : MythologyMayhem
{
    [SerializeField] Scene thisScene;


    public Dimension companionDimension;

    [SerializeField] public GameObject player2D;
    [SerializeField] public GameObject player3D;

    Rigidbody rb3D;
    Rigidbody2D rb2D;
    NavMeshAgent agent;
    Animator anim;
    [SerializeField] string attackTriggerName;

    public float speed = 9f;
    public float attackDamage = 5f;
    bool canAttack;
    [SerializeField] float attackTimer = 6f;
    [SerializeField] TriggerDetector attackTrigger;

    [SerializeField] float flipSensitivity = 5f;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] float playerRange = 2f;

    // Start is called before the first frame update
    void Start()
    {
        thisScene = SceneManager.GetActiveScene();

        GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");

        if (companionDimension == Dimension.TwoD)
        {
            int i = 0;

            do {
                if(_players[i].scene == this.gameObject.scene)
                {
                    player2D = _players[i];
                    Debug.Log("Found player. Scene: " + this.gameObject.scene.name);
                }
                i++;
            }
            while (i < _players.Length || player2D == null);
        } else
        {
            int i = 0;

            do
            {
                if (_players[i].scene == this.gameObject.scene)
                {
                    player3D = _players[i];
                    Debug.Log("Found player. Scene: " + this.gameObject.scene.name);
                }
                i++;
            }
            while (i < _players.Length || player3D == null);
        }

        anim = gameObject.GetComponent<Animator>();
    }//end start

    // Update is called once per frame
    void Update()
    {
        #region Use Player as Navigational Guide
        if (player2D != null)
        {
            //Flip Direction to look where the player is looking
            if (player2D.transform.rotation.y == 180)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            } else if (player2D.transform.rotation.y == 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            //Follow Player
            if(Vector2.Distance(player2D.gameObject.transform.position, gameObject.transform.position) < playerRange)
            {
                rb2D.MovePosition(Vector2.Lerp(gameObject.transform.localPosition, player2D.gameObject.transform.localPosition, speed * Time.deltaTime));
                anim.SetBool("Walking", true);
            } else
            {
                anim.SetBool("Walking", false);
            }
        }
        else if(player3D != null)
        {
            //Face wolf in same direction as player
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(gameObject.transform.rotation.x, player3D.transform.rotation.y, gameObject.transform.rotation.z));

            //Follow Player
            if(Vector3.Distance(player3D.gameObject.transform.position, gameObject.transform.position) < playerRange)
            {
                agent.SetDestination(player3D.gameObject.transform.localPosition);
                anim.SetBool("Walking", true);
            } else
            {
                anim.SetBool("Walking", false);
            }
        }
        else //Player is Unassigned
        {
            Debug.LogWarning("Player is not defined");
            Destroy(this.gameObject, 2f);
        }
        #endregion

        #region Detect Enemy for Attack

        if (attackTrigger.triggered)
        {
            GameObject enemy = attackTrigger.otherCollider2D.gameObject;

            if (player2D != null && enemy.tag == "Enemy")
            {
                if(Vector2.Distance(enemy.transform.position, gameObject.transform.position) < 3f)
                {
                    Attack(enemy);
                } else
                {
                    Vector2 xOnlyTargetPosition = new Vector2(enemy.transform.position.x, gameObject.transform.position.y);
                    gameObject.GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(gameObject.transform.position, xOnlyTargetPosition, speed * Time.deltaTime));
                }
            } 
            else if (player3D != null && attackTrigger.otherCollider3D.tag != "Player")
            {
                if (Vector3.Distance(enemy.transform.position, gameObject.transform.position) < 2f)
                {
                    Attack(enemy);
                }
                else
                {
                    Vector3 xOnlyTargetPosition = new Vector3(enemy.transform.position.x, gameObject.transform.position.y, enemy.transform.position.z);
                    gameObject.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(gameObject.transform.position, xOnlyTargetPosition, speed * Time.deltaTime));
                }
            }
        }
        #endregion

    }//end update

    public void Attack(GameObject enemy)
    {
        if(enemy.tag == "Enemy" && enemy.GetComponent<Health>())
        {
            anim.SetTrigger(attackTriggerName);
            enemy.GetComponent<Health>().TakeDamage(attackDamage);
            StartCoroutine(AttackRate(attackTimer));
        }
    }

    IEnumerator AttackRate(float timer)
    {
        canAttack = false;
        yield return new WaitForSeconds(timer);
        canAttack = true;
    }
}
