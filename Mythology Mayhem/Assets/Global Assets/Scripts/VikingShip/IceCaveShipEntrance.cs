using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCaveShipEntrance : MonoBehaviour
{
    public GameObject[] shipBorders;

    public Animator shipAnim;

    public string animationToTrigger;

    public Collider playerTrigger;

    public bool used;

    public LocalGameManager localGameManager;
    // Start is called before the first frame update
    void Start()
    {
        used = false;
        DeactivateBorders();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!used)
        {
            print(other.gameObject.name);
            if (other.tag == "Player")
            {
                GameObject tempPlayer = other.gameObject;
                foreach (GameObject obj in shipBorders) 
                {
                    obj.SetActive(true);
                }

                shipAnim.SetTrigger(animationToTrigger);
                playerTrigger.enabled = false;
                used = true;
                localGameManager.player.transform.parent.parent = null;
            }
        }
    }

    public void DeactivateBorders() 
    {
        foreach (GameObject obj in shipBorders) 
        {
            obj.SetActive(false);
        }
    }

    public void ResetTrigger(string nextAnim) 
    {
        used = false;
        playerTrigger.enabled = true;
        animationToTrigger = nextAnim;
    }
}
