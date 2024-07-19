using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Companion : MythologyMayhem
{
    public Health _health;
    public GameObject _player;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] float attackDamage;
    [SerializeField] float attackRate;
    float speed = 5f;
    bool canAttack = true;
    [SerializeField] TriggerDetector _td;
    [SerializeField] string walkingBool;
    [SerializeField] string attackTrigger;
    float lastXPosition;
    Vector3 lastPosition;
    [SerializeField] LocalGameManager localGameManager;

    public enum CompanionState { Following, Attacking };
    public CompanionState currentState = CompanionState.Following;

    private void Start()
    {
        //Ignore Player and Companion Collisions
        Physics2D.IgnoreLayerCollision(3, 14);

        //Save Health Component Reference
        try
        {
            _health = gameObject.GetComponent<Health>();
        }
        catch(NullReferenceException nre)
        {
            print(nre.Source + ": " + nre.Message);
            _health = gameObject.AddComponent<Health>();
            _health.Life = 10f;
        }

        if (GetComponent<Rigidbody2D>()) rb2D = GetComponent<Rigidbody2D>();
    }//end start

    private void Update()
    {
        if(currentState == CompanionState.Following)
        {
            //2D
            //Follow Player            
            if(rb2D != null && _player != null)
            {
                Vector2 xOnlyTargetPosition = new Vector2(_player.transform.position.x - 4f, gameObject.transform.position.y);
                rb2D.MovePosition(Vector2.Lerp(gameObject.transform.position, xOnlyTargetPosition, 2f));

                if (lastXPosition != gameObject.transform.position.x && walkingBool != "")
                {
                    anim.SetBool(walkingBool, true);
                }
                else if (walkingBool != "")
                {
                    anim.SetBool(walkingBool, false);
                }

                lastXPosition = gameObject.transform.position.x;

                //Flip Companion based on Player Sprite Renderer
                if (_player.GetComponentInChildren<SpriteRenderer>().flipX)
                {
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                } else
                {
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
            }
            
            //3D
            if(GetComponent<NavMeshAgent>())
            {
                //TODO: Add an Offset
                Vector3 xzOnlyTargetPosition = new Vector3(_player.transform.position.x - 6f, gameObject.transform.position.y, _player.transform.position.z - 6f);
                GetComponent<NavMeshAgent>().SetDestination(xzOnlyTargetPosition);

                if (lastPosition != gameObject.transform.position && walkingBool != "")
                {
                    anim.SetBool(walkingBool, true);
                }
                else if (walkingBool != "")
                {
                    anim.SetBool(walkingBool, false);
                }

                lastPosition = gameObject.transform.position;
            }

            //Switch State Condition
            if(_td.triggered)
            {
                if(gameObject.GetComponent<SpriteRenderer>() && _td.otherCollider2D.tag == "Enemy")
                {
                    currentState = CompanionState.Attacking;
                } else if (gameObject.GetComponent<NavMeshAgent>() && _td.otherCollider3D.tag == "Enemy")
                {
                    currentState = CompanionState.Attacking;
                }
            }
            
        }
        else //currentState == CompanionState.Attacking
        {
            //Attack Enemy
            if (_td.triggered)
            {
                //2D
                if(_td.otherCollider2D != null)
                {
                    Vector2 xOnlyTargetPosition = new Vector2(_td.otherCollider2D.transform.position.x - 2f, gameObject.transform.position.y);
                    GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(gameObject.transform.position, xOnlyTargetPosition, speed * Time.deltaTime));

                    if (canAttack && Vector3.Distance(gameObject.transform.position, _td.otherCollider2D.transform.position) <= 4f)
                    {
                        //Attack
                        anim.SetTrigger(attackTrigger);
                        _td.otherCollider2D.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
                    
                        StartCoroutine(AttackRate());
                    }
                }
                

                //3D
                if (_td.otherCollider3D != null && Vector3.Distance(gameObject.transform.position, _td.otherCollider3D.transform.position) > 4f)
                {
                    //Catch Up
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _td.otherCollider3D.transform.position, 4f);
                }
                else if (canAttack && _td.otherCollider3D != null && Vector3.Distance(gameObject.transform.position, _td.otherCollider3D.transform.position) <= 4f)
                {
                    //Attack
                    _td.otherCollider3D.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
                    StartCoroutine(AttackRate());
                }
            }

            //Switch State Condition
            if (!_td.triggered)
            {
                if (_td.otherCollider2D == null && _td.otherCollider3D == null)
                {
                    currentState = CompanionState.Following;
                }
            }
        }
    }//end update

    public IEnumerator AttackRate()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }

}//end companion script
