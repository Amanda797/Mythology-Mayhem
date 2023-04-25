using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingScrollBehavior : MonoBehaviour
{
    float countdown;
    int damage;
    // Start is called before the first frame update
    void Start()
    {
        countdown = 3f;
        damage = 2;
    }//end start

    // Update is called once per frame
    void Update()
    {        
        if(countdown <= 0) {
            Destroy(this.gameObject);
        } else {
            countdown -= 1 * Time.deltaTime;
        }
    }//end update
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == 3)
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            other.gameObject.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
        }
    }
}
