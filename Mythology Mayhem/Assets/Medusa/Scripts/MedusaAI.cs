using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MedusaAI : MonoBehaviour
{
    //Properties
    
    [SerializeField] NavMeshAgent medusaAgent;
    [SerializeField] MedusaControlScript mcs;
    [SerializeField] List<Transform> t;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] Health playerHealth;
    float timer = 0f;

    Vector3 lastPosition;

    private int waypointIndex = -1;
    [SerializeField] Vector3 target;

    void Awake() {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {        
        medusaAgent = gameObject.GetComponent<NavMeshAgent>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set Medusa Velocity and declare speeds
        Vector3 medusaVelocity;
        float forwardSpeed;
        float sideSpeed;

        //print("MV Before: " + medusaAgent.velocity);
        medusaVelocity = (medusaAgent.transform.position - lastPosition) /  Time.deltaTime;
        print("MV After: " + medusaVelocity);
        lastPosition = medusaAgent.transform.position;

        forwardSpeed = medusaAgent.transform.InverseTransformDirection(medusaVelocity).z / medusaAgent.speed;
        sideSpeed = medusaAgent.transform.InverseTransformDirection(medusaVelocity).x / medusaAgent.speed;

        mcs.forwardSpeed = forwardSpeed;
        mcs.sideSpeed = sideSpeed;

        SetDestination(playerHealth);
        /*
        if(timer >= 5) {
            waypointIndex = (int) Random.Range(0, waypoints.Count);
            SetDestination(waypoints[waypointIndex]); 
            timer = 0f;
        } else {
            timer += 1 * Time.deltaTime;
        }
        */

    }//end update

    public void SetDestination(Transform point) {
        medusaAgent.SetDestination(point.position);
    }

    public void SetDestination(Health _playerHealth) {
        medusaAgent.SetDestination(_playerHealth.gameObject.transform.position);
    }

}
