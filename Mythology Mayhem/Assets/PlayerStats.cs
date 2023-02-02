using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private BoxCollider2D damageCollider;

    [Header("Player Stats")]
    [SerializeField] private float atkDamage = 1f;
    [SerializeField] private float health = 10f;

    [SerializeField] private bool canHit = false;
    public GameObject[] enemies;
    public GameObject test;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
        }
    }     
    private void CanAttack() 
    {
        damageCollider.gameObject.tag = "CanDamage";
    }
    private void CannotAttack() 
    {
        damageCollider.gameObject.tag = "Untagged";
    }
}
