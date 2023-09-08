using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsActions : EnemyActions
{
    [Header("Special Action")]
    [SerializeField] GameObject snowballProjectile;
    [SerializeField] float velocity = 500f;

    public new void SpecialAbility() {
        //Customizable Special Ability
        GameObject snowball = Instantiate(snowballProjectile, transform.position, transform.rotation);
        snowball.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(0,velocity,0));
    }//end special ability void

}
