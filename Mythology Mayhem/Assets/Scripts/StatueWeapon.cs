using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueWeapon : MonoBehaviour
{
    public StatuePuzzle statueManager;
    public int weaponElement;
    private SpriteRenderer weaponSprite;

    private Transform player;

    public float interactDistance;
    private bool pickedUp = false; 

    public bool deBugStatueWeaponReset = false;

    // Start is called before the first frame update
    void Start()
    {
        weaponSprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(deBugStatueWeaponReset)
        {
            PlayerPrefs.SetInt("carriedWeapon", 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) < interactDistance && !pickedUp)
        {
            Debug.Log("Player in Range");

            if(Input.GetKeyDown(KeyCode.E)) //&& PlayerPrefs.GetInt("carriedWeapon") == 0)
            {
                PlayerPrefs.SetInt("carriedWeapon", weaponElement + 1);
            }
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < statueManager.statues.Count; i++)
        {
            string statueWeaponCheck = "statueWeapon" + i;
            pickedUp = PlayerPrefs.GetInt("carriedWeapon") == weaponElement + 1 || PlayerPrefs.GetInt(statueWeaponCheck) == weaponElement + 1;
            if(pickedUp)
            {
                break;
            }
        }

        if(pickedUp)
        {
            transform.GetComponent<SpriteRenderer>().enabled = false;
            transform.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().enabled = true;
            transform.GetComponent<Collider2D>().enabled = true;
        }
    }
}
