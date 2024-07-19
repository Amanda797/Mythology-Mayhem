using UnityEngine;

public class FallingScrollBehavior : MonoBehaviour
{
    [SerializeField] int damage = 2;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.name == "ScrollCollider") Destroy(this.gameObject);

        if (other.gameObject.layer == 3)
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            other.gameObject.GetComponent<KnockBackFeedback>().PlayerFeedback(gameObject);
        }
    }
}
