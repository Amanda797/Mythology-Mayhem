using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Companion : MythologyMayhem
{
    AudioSource audioSource;
    Rigidbody2D rb2D;
    NavMeshAgent agent;
    Animator anim;
    TriggerDetector triggerDetector;

    public Health _health;
    public GameObject _player;
    public AudioClip[] audioClips;
    [SerializeField] float attackDamage;
    [SerializeField] float attackRate;
    float speed = 5f;
    bool canAttack = true;
    [SerializeField] string walkingBool;
    [SerializeField] string attackTrigger;
    float lastXPosition;
    Vector3 lastPosition;
    public LocalGameManager localGameManager;

    public enum CompanionState { Following, Attacking };
    public CompanionState currentState = CompanionState.Following;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        _health = gameObject.GetComponent<Health>();
        anim = gameObject.GetComponent<Animator>();
        triggerDetector = gameObject.GetComponentInChildren<TriggerDetector>();
        _player = localGameManager.player.gameObject;
        if (GetComponent<Rigidbody2D>()) rb2D = GetComponent<Rigidbody2D>();
        if (GetComponent<NavMeshAgent>()) agent = GetComponent<NavMeshAgent>(); 
    }
    private void Start()
    {
        // Ignore Player and Companion Collisions
        if (rb2D != null) Physics2D.IgnoreLayerCollision(3, 14);
        if (agent != null) Physics.IgnoreLayerCollision(3, 14);
    }

    private void Update()
    {
        if(currentState == CompanionState.Following)
        {
            //2D
            if(rb2D != null && _player != null)
            {
                Vector2 xOnlyTargetPosition = new Vector2(_player.transform.position.x - 2f, transform.position.y);
                rb2D.MovePosition(Vector2.Lerp(transform.position, xOnlyTargetPosition, 2f));

                //Flip Companion based on Player Sprite Renderer
                if (_player.GetComponentInChildren<SpriteRenderer>().flipX) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                else transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            
            //3D
            else if(agent != null && _player != null)
            {
                //TODO: Add an Offset
                Vector3 xzOnlyTargetPosition = new Vector3(_player.transform.position.x - 2f, transform.position.y, _player.transform.position.z - 2f);
                agent.SetDestination(xzOnlyTargetPosition);
            }

            if (lastPosition != transform.position && walkingBool != "")
            {
                anim.SetBool(walkingBool, true);
            }
            else if (walkingBool != "")
            {
                anim.SetBool(walkingBool, false);
            }

            lastPosition = transform.position;

            //Switch State Condition
            if (triggerDetector.triggered) if(triggerDetector.otherCollider2D.tag == "Enemy") currentState = CompanionState.Attacking;
        }
        else //currentState == CompanionState.Attacking
        {
            if (!canAttack) return;
            //Attack Enemy
            if (triggerDetector.triggered)
            {
                //2D
                if(triggerDetector.otherCollider2D != null)
                {
                    Vector2 xOnlyTargetPosition = new Vector2(triggerDetector.otherCollider2D.transform.position.x - 2f, transform.position.y);
                    rb2D.MovePosition(Vector2.Lerp(transform.position, xOnlyTargetPosition, speed * Time.deltaTime));

                    if (Vector3.Distance(transform.position, triggerDetector.otherCollider2D.transform.position) <= 4f)
                    {
                        //Attack
                        anim.SetTrigger(attackTrigger);
                        triggerDetector.otherCollider2D.gameObject.GetComponent<Health>().TakeDamage(attackDamage);

                        audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
                        audioSource.Play();
                    
                        StartCoroutine(AttackRate());
                    }
                }                

                //3D
                else if (triggerDetector.otherCollider3D != null && Vector3.Distance(transform.position, triggerDetector.otherCollider3D.transform.position) > 4f)
                {
                    //Catch Up
                    transform.position = Vector3.Lerp(transform.position, triggerDetector.otherCollider3D.transform.position, 4f);
                }
                else if (triggerDetector.otherCollider3D != null && Vector3.Distance(transform.position, triggerDetector.otherCollider3D.transform.position) <= 4f)
                {
                    //Attack
                    triggerDetector.otherCollider3D.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
                    StartCoroutine(AttackRate());
                }
            }

            //Switch State Condition
            else if (triggerDetector.otherCollider2D == null && triggerDetector.otherCollider3D == null) currentState = CompanionState.Following;
        }
    }

    public IEnumerator AttackRate()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }

}
