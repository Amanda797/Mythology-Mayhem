using UnityEngine;

public class MouseAI : MonoBehaviour
{
    [Header("Mouse Movement")]
    [SerializeField] private float walkSpeed = 5f;
    //[SerializeField] private float runSpeed = 10f;

    [Header("Attack Activation")]
    [SerializeField] private BoxCollider2D soundTrigger;
    [SerializeField] private GameObject attackTarget;

    [Header("Mouse Animations")]
    [SerializeField] private Animator mouseAnim;

    [Header("Mouse Waypoints")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int waypointIndex = 0;

    private Transform[] savedWaypoints;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        savedWaypoints = waypoints;
        if (attackTarget == null) 
        {
            attackTarget = GameObject.FindWithTag("Player");
        }
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveMouse();
        FlipMouseSpriteToWalkDirection();
        AttackPlayer();
    }

    void MoveMouse()
    {
        mouseAnim.SetBool("IsPatrolling", true);
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, walkSpeed * Time.deltaTime);
        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    void FlipMouseSpriteToWalkDirection()
    {
        //TODO: Flip the mouse sprite in the direction it is walking
        return;
    }
    void AttackPlayer()
    {
        bool attackTrigger = soundTrigger.IsTouching(attackTarget.GetComponent<Collider2D>());
        if (attackTrigger)
        {
            //Debug.Log("ATTACK GREEK BOY!");
        }
        mouseAnim.SetBool("IsAttacking", attackTrigger);
    }
}
