using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenHeadScript : MonoBehaviour
{
    public State currentState;

    public Transform underWaterPoint;
    public Transform aboveWaterPoint;

    public int round;
    public float speed;

    public Animator anim;

    public GameObject tentaclePrefab;

    public List<KrakenTentacleScript> spawnedTentacles;
    public int currentTentacle;

    public Transform[] tentacleStartPositions;
    public Transform[] tentacleGrabPositions;
    public List<int> remainingIndexes;

    public enum State 
    { 
        Underwater,
        Risen,
        Tentacles,
        Waiting,
        Defeated
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(State.Underwater);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.Risen)
        {
            float dist = Vector3.Distance(transform.position, aboveWaterPoint.position);
            if (dist > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, aboveWaterPoint.position, speed * Time.deltaTime);
            }
            else 
            {
                switch (round)
                {
                    case 1:
                        GenerateNewIndex();
                        SpawnTentacles(3);
                        break;
                    case 2:
                        GenerateNewIndex();
                        SpawnTentacles(4);
                        break;
                    case 3:
                        GenerateNewIndex();
                        SpawnTentacles(6);
                        break;
                    case 4:
                        GenerateNewIndex();
                        SpawnTentacles(8);
                        break;
                }

                ChangeState(State.Tentacles);
            }
        }
        else if(currentState == State.Defeated)
        {
            float dist = Vector3.Distance(transform.position, underWaterPoint.position);
            if (dist > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, underWaterPoint.position, speed * Time.deltaTime);
            }
        }

        if (currentState == State.Tentacles) 
        {
            if (spawnedTentacles.Count <= 0)
            {
                ChangeState(State.Defeated);
            }
            else
            {
                bool checkWaiting = true;
                for (int i = 0; i < spawnedTentacles.Count; i++)
                {
                    if (spawnedTentacles[i].currentState != KrakenTentacleScript.State.Waiting)
                    {
                        checkWaiting = false;
                        break;
                    }
                }
                if (checkWaiting)
                {
                    ChooseNextTentactle();
                    ChangeState(State.Waiting);
                }
            }
        }
    }
    void ChangeState(State newState) 
    {
        currentState = newState;
        switch (currentState) 
        {
            case State.Underwater:

                break;
            case State.Risen:

                break;
            case State.Tentacles:

                break;
            case State.Defeated:

                break;
        }
    }

    public void SummonKraken(int round) 
    {
        this.round = round;
        ChangeState(State.Risen);
    }
    void SpawnTentacles(int count) 
    {
        for (int i = 0; i < count; i++) 
        {
            int pick = (int)Random.Range(0, remainingIndexes.Count);
            int which = remainingIndexes[pick];
            remainingIndexes.RemoveAt(pick);
            SpawnTentacle(which);
        }
    }
    void SpawnTentacle(int pos) 
    {
        GameObject obj = Instantiate(tentaclePrefab, tentacleStartPositions[pos].position + new Vector3(0,-20,0), tentacleStartPositions[pos].rotation);
        KrakenTentacleScript tentacleScript = obj.GetComponent<KrakenTentacleScript>();

        if (tentacleScript != null) 
        {
            tentacleScript.risePosition = tentacleStartPositions[pos];
            tentacleScript.attachPosition = tentacleGrabPositions[pos];
            tentacleScript.headScript = this;
            tentacleScript.Summon();
            spawnedTentacles.Add(tentacleScript);
        }
    }

    void ChooseNextTentactle() 
    {
        print("Choosing");
        if(spawnedTentacles.Count > 0)
        { 
            int index = (int)Random.Range(0, spawnedTentacles.Count);
            currentTentacle = index;
            spawnedTentacles[index].Select();
            spawnedTentacles.RemoveAt(index);
        }
        else 
        {
            ChangeState(State.Defeated);
        }
    }
    public void TentacleHurt() 
    {
        anim.SetTrigger("Hurt");
    }
    public void TentacleDeath() 
    {
        ChangeState(State.Tentacles);
    }

    void GenerateNewIndex() 
    {
        List<int> tempList = new List<int>();

        for (int i = 0; i < 8; i++) 
        {
            tempList.Add(i);
        }
        remainingIndexes = tempList;
    }
}
