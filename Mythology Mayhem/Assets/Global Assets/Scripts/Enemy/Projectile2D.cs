using UnityEngine;

public class Projectile2D : MonoBehaviour
{

    [SerializeField]  float Speed;
    [SerializeField]  int Damage;
    [SerializeField] float _destroyTimer;
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
        transform.position += -transform.right * Time.deltaTime * Speed;
    }


    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
