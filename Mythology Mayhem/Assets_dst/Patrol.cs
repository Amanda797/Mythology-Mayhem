using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] Transform[] waypoints;
    [SerializeField] float speed;
    int waypointIndex;
    float idleTimer;
    bool patrolling;
    bool reachedDestination;

   
}
