using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float MaxHealth;
    [SerializeField] private float health;
    [SerializeField] Behaviour[] components;
    [SerializeField] GameObject mainObject; // Parent Self

    [Header("Animation")]
    [SerializeField] Animator anim;
    [SerializeField] string hurtAnim;
    [SerializeField] string deathAnim;
    [SerializeField] string healAnim;

    void Start()
    {
        health = MaxHealth;
    }

    void Update() {
        // test
        health -= 1f * Time.deltaTime;
        //print(health);

        if(health <= 0) {
            Death();
        }
    }

    public void SetHealth(float h) {
        health += h;
    }

    public float GetHealth() {
        return health;
    }

    public void TakeDamage(float d) {
        anim.SetTrigger(hurtAnim);
        SetHealth(-d);
    }

    public void Heal(float h) {
        anim.SetTrigger(healAnim);
        SetHealth(h);
    }

    public void Death() {
        anim.SetTrigger(deathAnim);       
        //this? 
        foreach (Behaviour component in components) {
            component.enabled = false;
        }
        //or this??
        Destroy(mainObject, 3f);
    }

}
