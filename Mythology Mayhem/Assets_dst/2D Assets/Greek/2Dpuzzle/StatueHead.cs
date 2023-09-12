using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueHead : MonoBehaviour
{

    public StatuePuzzle3D statueManager;
    public int headElement;
    //private MeshFilter meshFilter;

    private Transform player;

    public float interactDistance;
    private bool pickedUp = false; 

    public bool deBugStatueHeadReset = false;
    public GameObject model;

    // Start is called before the first frame update
    void Start()
    {
        //meshFilter = GetComponent<MeshFilter>();
        //meshFilter.mesh = statueManager.heads[headElement];
         var playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (var playerObject in playerObjects)
        {
            if (playerObject.GetComponent<CharacterController>())
            {
                player = playerObject.transform;
                break;
            }
        }

        if(deBugStatueHeadReset)
        {
            PlayerPrefs.SetInt("carriedHead", 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) < interactDistance && !pickedUp)
        {
            Debug.Log("Player in Range");

            if(Input.GetKeyDown(KeyCode.E)) //&& PlayerPrefs.GetInt("carriedHead") == 0)
            {
                PlayerPrefs.SetInt("carriedHead", headElement + 1);
            }
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < statueManager.statues.Count; i++)
        {
            string statueBodyCheck = "statueBody" + i;
            pickedUp = PlayerPrefs.GetInt("carriedHead") == headElement + 1 || PlayerPrefs.GetInt(statueBodyCheck) == headElement + 1;
            if(pickedUp)
            {
                break;
            }
        }

        if(pickedUp)
        {
            model.SetActive(false);
            //transform.GetComponent<MeshRenderer>().enabled = false;
            transform.GetComponent<Collider>().enabled = false;
        }
        else
        {
            model.SetActive(true);
            //transform.GetComponent<MeshRenderer>().enabled = true;
            transform.GetComponent<Collider>().enabled = true;
        }
    }
}