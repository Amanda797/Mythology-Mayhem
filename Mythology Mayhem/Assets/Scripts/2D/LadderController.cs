using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject ladder1;
    [SerializeField] private GameObject ladder2;

    // Update is called once per frame
    void Update()
    {
        if (ladder1.GetComponent<Ladder>().entered || ladder2.GetComponent<Ladder>().entered)
        {
            floor.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            floor.GetComponent<Collider2D>().enabled = true;
        }
    }
}
