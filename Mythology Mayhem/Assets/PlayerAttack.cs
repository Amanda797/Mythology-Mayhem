using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public AnimationClip attackAnim;
    public Attack3D attack3D;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.Play(attackAnim.name);
        }
    }
    public void Attack()
    {
        attack3D.Attack();
    }
    public void AttackEnd()
    {
        attack3D.StopAttack();
    }
}
