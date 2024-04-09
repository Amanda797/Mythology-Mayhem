using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorsHammer : MonoBehaviour
{
    bool is3D;

    public PlayerMovement3D playerMovement;
    public Animator anim;
    public string attackTrigger;
    public Attack3D attack3D;
    public float hammerDamage = 20f;
    public float throwForce = 300f;

    public GameObject ThorHammer2D;
    public GameObject ThorHammer3D;

    public float hammerTimer = 15f;
    float hammerTime; //Haha
    bool hammerCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        //attack3D.damage = hammerDamage;
    }

    // Update is called once per frame
    void Update()
    {
        //3D Hammer
        if(is3D)
        {
            if (!playerMovement.frozen)
            {
                if (Input.GetMouseButtonDown(0) && !hammerCooldown)
                {
                    //anim.SetTrigger(attackTrigger);

                    //attack3D.Attack();

                    //Spawn Throwing Hammer 3D
                    GameObject hammer3D = Instantiate(ThorHammer3D, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                    hammer3D.transform.rotation.SetLookRotation(transform.forward);

                    //Throw at Enemy
                    if (hammer3D.GetComponent<Rigidbody>())
                    {
                        hammer3D.GetComponent<Rigidbody>().AddForce(throwForce * hammer3D.transform.position, ForceMode.Force);
                    }

                    hammerCooldown = true;
                }
            }
        } else
        //2D Hammer
        {
            if (Input.GetMouseButtonDown(0) && !hammerCooldown)
            {
                //Spawn Throwing Hammer 2D
                GameObject hammer2D = Instantiate(ThorHammer2D, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                hammer2D.transform.rotation.SetLookRotation(transform.forward);

                //Throw at Enemy
                if (hammer2D.GetComponent<Rigidbody2D>())
                {
                    hammer2D.GetComponent<Rigidbody2D>().AddForce(throwForce * hammer2D.transform.position, ForceMode2D.Force);
                }

                hammerCooldown = true;
            }
            
        }
        
        //Cooldown Timer
        if(hammerCooldown)
        {
            hammerTime += Time.deltaTime;

            if(hammerTime >= hammerTimer)
            {
                hammerCooldown = false;
            }
        }
    }//end update
}
