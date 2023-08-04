using UnityEngine;

public class PlayerController3D : MonoBehaviour
{

    [SerializeField] 
    private int health = 20;

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            //gameObject.GetComponent<Renderer>().enabled = false;
            //gameObject.GetComponent<Collider>().enabled = false;
            //Destroy(this);
            health = 0;
        }
    }

    public void DamageHealth(int dmg) {
        health -= dmg;
        Debug.Log("Health: " + health);
    }

    public int GetHealth() {
        return health;
    }

}
