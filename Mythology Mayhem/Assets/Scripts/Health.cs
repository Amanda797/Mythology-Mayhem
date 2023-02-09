using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float MaxHealth;
    [SerializeField] private float health;
    [SerializeField] Behaviour[] components;


    public void SetHealth(float h) {
        health += h;
    }

    public float GetHealth() {
        return health;
    }

    public void TakeDamage(float d) {
        health -= d;
    }

    public void Heal(float h) {
        health += h;
        //anim.SetTrigger("hurt"); //all actors that use this health script should have an animation clip called hurt in their animation controller
    }

    void Start()
    {
        health = MaxHealth;
    }

    void Update() {
        //health -= 1f * Time.deltaTime;
        //print(health);
    }

    public void Death() {
        
        foreach (Behaviour component in components) {
            component.enabled = false;
        }
    }

}
