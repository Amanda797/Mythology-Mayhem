using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    GameObject _host;
    bool _hostIsEnemy;
    Vector3 _initialPosition;
    [SerializeField] float _speed;
    [SerializeField] int _damage;
    [SerializeField] float _destroyTimer;
    public float DestroyTimer { get => _destroyTimer; set => _destroyTimer = value; }
    private float _destroyCount;
    public bool Parryable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _destroyCount = 0;
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _destroyCount += Time.deltaTime;
        if(_destroyCount >= DestroyTimer)
        {
            ResetProjectile();
        }
        transform.position += -transform.right * Time.deltaTime * _speed;
    }

    public void Parry() { }

    void ResetProjectile()
    {
        transform.position = _initialPosition;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(_damage);
            ResetProjectile();
        }

        if(collision.gameObject.GetComponentInChildren<Enemy>() && collision.gameObject != _host)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
            ResetProjectile();
        }
    }
}
