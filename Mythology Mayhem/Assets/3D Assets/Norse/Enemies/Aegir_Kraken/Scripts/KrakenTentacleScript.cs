using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenTentacleScript : MonoBehaviour
{
    public State currentState;
    public int health;

    public Transform risePosition;
    public Transform attachPosition;
    public float speed;
    public float deathspeed;

    public Animator anim;
    public KrakenHeadScript headScript;

    public bool bypassDamage;

    public enum State 
    { 
        Underwater,
        Risen,
        Waiting,
        Choosen,
        Attached,
        Dying,
        Dead
    }
    // Start is called before the first frame update
    void Awake()
    {
        currentState = State.Underwater;   
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.Risen) {
            if (Vector3.Distance(transform.position, risePosition.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, risePosition.position, speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("Rise", false);
                ChangeState(State.Waiting);
            }
        }

        if (currentState == State.Choosen)
        {
            if (Vector3.Distance(transform.position, attachPosition.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, attachPosition.position, speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("Grab", true);
                ChangeState(State.Attached);
            }
        }
        if (currentState == State.Attached) 
        {
            if (bypassDamage) 
            {
                Damage(health);
                bypassDamage = false;
            }
        }
        if (currentState == State.Dying) 
        {
            if (Vector3.Distance(transform.position, risePosition.position + new Vector3(0, -10, 0)) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, risePosition.position + new Vector3(0, -10, 0), deathspeed * Time.deltaTime);
            }
            else 
            {
                Death();
                ChangeState(State.Dead);
            }
        }

    }

    void ChangeState(State newState) 
    {
        currentState = newState;

        switch (currentState) 
        {
            case State.Underwater:
                break;
            case State.Risen:
                anim.SetBool("Rise", true);
                break;
            case State.Attached:
                break;
            case State.Dead:
                break;
        }
    }
    public void Summon() 
    {
        ChangeState(State.Risen);
    }
    public void Select() 
    {
        ChangeState(State.Choosen);
    }
    public void Damage(int amount) 
    {
        if (currentState == State.Attached)
        {
            health -= amount;
            if (health <= 0)
            {
                health = 0;
                anim.SetTrigger("Hurt");
                anim.SetBool("Dead", true);
                headScript.TentacleHurt();
                headScript.TentacleDeath();
            }
        }
    }

    public void Death() 
    {
        Destroy(this.gameObject, 1.0f);
    }

    public void AnimDeath() 
    {
        if (health <= 0)
        {
            ChangeState(State.Dying);
        }
    }
}
