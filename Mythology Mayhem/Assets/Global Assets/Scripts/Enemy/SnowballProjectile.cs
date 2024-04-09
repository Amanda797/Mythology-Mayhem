using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] float _projectileDamage;
    [SerializeField] bool _parryable;
    [SerializeField] float _destroyTimer;
    public Enemy enemy;

    public float ProjectileDamage { get => _projectileDamage; set => _projectileDamage = value; }
    public bool Parryable { get => _parryable; set => _parryable = value; }
    public float DestroyTimer { get => _destroyTimer; set => _destroyTimer = value; }
    private float DestroyCount;

    // Start is called before the first frame update
    void Start()
    {
        DestroyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyCount += Time.deltaTime;
        if(DestroyCount >= DestroyTimer)
        {
            Destroy(gameObject);
        }
        //Parry();
    }

    public void Parry() { 
    // Reverse direction of snowball
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<FPSHealth>().TakeDamage(enemy.attackDamage);
        } else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(enemy.attackDamage);
        }
    }
}
