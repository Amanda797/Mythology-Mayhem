using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueBody3D : MonoBehaviour
{
    public MeshFilter headMeshFilter;
    public StatuePuzzle3D statueManager;
    private Transform player;
    public float interactDistance;
    public int statueElement;
    public StatueBody2D statueBody2D;

    public enum ChangeHeadMethod
    {
        MeshFilter, SetActive

    }

    public ChangeHeadMethod changeHeadMethod;
    public List <GameObject> heads = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        var playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (var playerObject in playerObjects)
        {
            if (playerObject.GetComponent<CharacterController>())
            {
                player = playerObject.transform;
                break;
            }
        }

        /*StatueBody2D[] allstatueBodies2D = FindObjectsOfType<StatueBody2D>();

        foreach (StatueBody2D statueBody in allstatueBodies2D)
        {
            if(statueBody.statueElement == statueElement)
            {
                this.statueBody2D = statueBody;
                break;
            }
        }*/

        ChangeHeads();

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) < interactDistance)
        {
            Debug.Log("Player in Range");

            if(Input.GetKeyDown(KeyCode.E))
            {
                if(PlayerPrefs.GetInt(StatueBodyCheck()) != 0 && PlayerPrefs.GetInt("carriedHead") > 0)
                {
                    //return;
                    int carriedHead = PlayerPrefs.GetInt("carriedHead");
                    int statueHead = PlayerPrefs.GetInt(StatueBodyCheck());
                    PlayerPrefs.SetInt("carriedHead", statueHead);
                    PlayerPrefs.SetInt(StatueBodyCheck(), carriedHead);
                    ChangeHeads();
                }
                else if(PlayerPrefs.GetInt("carriedHead") != 0)
                {
                    //statueManager.statues[statueElement].currentHead = PlayerPrefs.GetInt("carriedHead") - 1;
                    PlayerPrefs.SetInt(StatueBodyCheck(), PlayerPrefs.GetInt("carriedHead"));
                    PlayerPrefs.SetInt("carriedHead", 0);
                    ChangeHeads();
                }
                else if(PlayerPrefs.GetInt(StatueBodyCheck()) > 0)
                {
                    PlayerPrefs.SetInt("carriedHead", PlayerPrefs.GetInt(StatueBodyCheck()));
                    PlayerPrefs.SetInt(StatueBodyCheck(), 0);
                    ChangeHeads();
                }
            }

        }
    }

    public void ChangeHeads()
    {
        
        if(changeHeadMethod == ChangeHeadMethod.MeshFilter)
        {
            ChangeHeadsMeshFilter();
        }
        else if(changeHeadMethod == ChangeHeadMethod.SetActive)
        {
            ChangeHeadsSetActive();
        }

        statueManager.CheckHeadPuzzleStatus();
        statueBody2D.ChangeHeads();
    }

    void ChangeHeadsMeshFilter()
    {
        if(PlayerPrefs.GetInt(StatueBodyCheck()) != statueManager.statues[statueElement].currentHead)
        {
            statueManager.statues[statueElement].currentHead = PlayerPrefs.GetInt(StatueBodyCheck());
        }

        if(statueManager.statues[statueElement].currentHead <= 0 || statueManager.statues[statueElement].currentHead > statueManager.heads.Count)
        {
            headMeshFilter.mesh = null;
        }
        else
        {
            headMeshFilter.mesh = statueManager.heads[statueManager.statues[statueElement].currentHead - 1];
        }
    }

    void ChangeHeadsSetActive()
    {
        foreach(GameObject head in heads)
        {
            head.SetActive(false);
        }

        if(PlayerPrefs.GetInt(StatueBodyCheck()) != statueManager.statues[statueElement].currentHead)
        {
            statueManager.statues[statueElement].currentHead = PlayerPrefs.GetInt(StatueBodyCheck());
        }

        if(statueManager.statues[statueElement].currentHead <= 0 || statueManager.statues[statueElement].currentHead > heads.Count)
        {
            return;
        }
        else
        {
            heads[statueManager.statues[statueElement].currentHead - 1].SetActive(true);
        }
    
    }

    string StatueWeaponCheck()
    {
        return "statueWeapon" + statueElement;
    }

    string StatueBodyCheck()
    {
        return "statueBody" + statueElement;
    }

}
