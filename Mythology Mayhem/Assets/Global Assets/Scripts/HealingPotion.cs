using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private PlayerStats player;
    [SerializeField] private bool smallPotion;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == 3)
        {
            if (smallPotion) 
                other.gameObject.GetComponent<PlayerStats>().Heal(2);
            else
                other.gameObject.GetComponent<PlayerStats>().Heal(4);
            
            if(gameObject.GetComponent<AudioSource>())
                gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }  
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer == 3)
        {
            if (smallPotion) 
                other.gameObject.GetComponent<PlayerStats>().Heal(2);
            else
                other.gameObject.GetComponent<PlayerStats>().Heal(4);
            
            if(gameObject.GetComponent<AudioSource>())
                gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }  
    }
}
