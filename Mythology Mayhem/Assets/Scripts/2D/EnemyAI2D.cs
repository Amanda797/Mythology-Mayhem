using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI2D : MonoBehaviour
{
    int speed;
    bool alive;

    private Health health;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(alive) {
            if(health.GetHealth() <= 0) {
                alive = false;
                Death();
            }
        }
    }

    void Death() {
        anim.SetTrigger("dying");
    }
}
