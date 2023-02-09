using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] Transform leftEdge;
    [SerializeField] Transform rightEdge;

    [Header ("Enemy")]
    [SerializeField] Transform enemy;
    [SerializeField] float speed;
    [SerializeField] float speedMultiplier = 15f;

    [SerializeField] Vector3 initScale;
    private bool movingLeft;

    [SerializeField] Animator anim;

    [SerializeField] float idleTimer = 0f;
    [SerializeField] float idleDuration = 5f;

    void Awake() {
        initScale = enemy.localScale;
    }

    private void MoveInDirection(int _direction) {
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed * speedMultiplier, enemy.position.y, enemy.position.z);
        //print(enemy.position);

        anim.SetBool("moving", true);
    }

    private void DirectionChange() {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        if(idleTimer >= idleDuration) {
            idleTimer = 0f;
            movingLeft = !movingLeft;
        }
        
    }

    void Update() {
        // Move according to patrol points, turn according to direction
        if(movingLeft){
            if(enemy.position.x >= leftEdge.position.x) {
                    MoveInDirection(-1);
            } else {
                DirectionChange();
            }
        } else {
            if(enemy.position.x <= rightEdge.position.x) {
                    MoveInDirection(1);
            } else {
                DirectionChange();
            }
        }
        
    }
}
