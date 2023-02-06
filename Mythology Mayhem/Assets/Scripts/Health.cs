using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float MaxHealth;
    private float health;

    public void SetHealth(float h) {
        health += h;
    }

    public float GetHealth() {
        return health;
    }

    void Start()
    {
        health = MaxHealth;
    }

    void Update() {
        health -= 1f * Time.deltaTime;
        print(health);
    }

}
