using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3DSight : MonoBehaviour
{
    EnemyAI3D enemyAI;

    void Start() {
        enemyAI = gameObject.GetComponentInParent<EnemyAI3D>();
    }    

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player" && !enemyAI.isAttacking) {
            Debug.Log("Hit Player!");
            enemyAI.isAttacking = true;
            enemyAI.target = col.gameObject.transform.position;
            enemyAI.player = col;        
        }
    }

    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player" && enemyAI.isAttacking) {
            Debug.Log("No Player!");
            enemyAI.isAttacking = false;
            enemyAI.player = null;
        }
    }
}
