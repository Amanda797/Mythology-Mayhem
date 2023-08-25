using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineMMSlotScript : MonoBehaviour
{
    public int x;
    public int y;

    public Vector3 startVector;
    public float yInactivePos;
    public float yActivePos;
    public float speed;

    public NineMMMarbleScript occupying;
    public Renderer rend;

    public Collider col;

    public Transform marblePos;

    public Selection selection;
    public bool finishedShifting;

    public bool selectionSlot;

    public enum Selection 
    { 
        Active,
        Inactive
    }
    private void Start()
    {
        startVector = transform.position;
    }
    private void Update()
    {
        if (selectionSlot)
        {
            if (selection == Selection.Active && !finishedShifting)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, startVector.y + yActivePos, transform.position.z), step);
                float dist = Vector3.Distance(transform.position, new Vector3(transform.position.x, startVector.y + yActivePos, transform.position.z));
                if (dist <= 0.001f)
                {
                    finishedShifting = true;
                }
            }
            else if (selection == Selection.Inactive && !finishedShifting)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, startVector.y + yInactivePos, transform.position.z), step);

                float dist = Vector3.Distance(transform.position, new Vector3(transform.position.x, startVector.y + yInactivePos, transform.position.z));
                if (dist <= 0.001f)
                {
                    col.enabled = false;
                    finishedShifting = true;
                }
            }
        }
    }
    public void Activate(bool enableCollider)
    {
        selection = Selection.Active;
        col.enabled = true;
        finishedShifting = false;
    }
    public void Deactivate()
    {
        selection = Selection.Inactive;
        col.enabled = false;
        finishedShifting = false;
    }
}
