using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCaveAnim : MonoBehaviour
{
    public Vector3 startingPos;
    public Vector3 lastPos;
    public Vector3 curRandomMovement;
    public float XZVariable;
    public float XZVariable2;

    public float timeStamp;
    public float curTimer;
    public float baseTimer;
    public float timerVariable;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.localPosition;
        lastPos = startingPos;
        CreateNewTimer();
    }

    // Update is called once per frame
    void Update()
    {
        float curTime = Time.time - timeStamp;
        if (curTime < curTimer)
        {
            transform.localPosition = Vector3.Lerp(lastPos, curRandomMovement, curTime / curTimer);
        }
        else 
        {
            CreateNewTimer();
        }
    }

    void CreateNewTimer() 
    {
        timeStamp = Time.time;
        curTimer = baseTimer + Random.Range(-timerVariable, timerVariable);

        curRandomMovement = startingPos + new Vector3(Random.Range(-XZVariable, XZVariable), 0, Random.Range(-XZVariable, XZVariable));
        lastPos = transform.localPosition;
    }
}
