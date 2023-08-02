using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueBody2D : MonoBehaviour
{
    public SpriteRenderer bodySpriteRenderer;
    public SpriteRenderer headSpriteRenderer;
    public StatuePuzzle statueManager;
    private Transform player;
    public float interactDistance;
    public int statueElement;

    public Sprite headlessBodySprite;
    public List<Sprite> weaponBodySprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
                if(PlayerPrefs.GetInt(StatueWeaponCheck()) != 0 && PlayerPrefs.GetInt("carriedWeapon") > 0)
                {
                    //return;
                    int carriedWeapon = PlayerPrefs.GetInt("carriedWeapon");
                    int statueWeapon = PlayerPrefs.GetInt(StatueWeaponCheck());
                    PlayerPrefs.SetInt("carriedWeapon", statueWeapon);
                    PlayerPrefs.SetInt(StatueWeaponCheck(), carriedWeapon);
                    ChangeWeapons();
                }
                else if(PlayerPrefs.GetInt("carriedWeapon") != 0)
                {
                    //statueManager.statues[statueElement].currentWeapon = PlayerPrefs.GetInt("carriedWeapon") - 1;
                    PlayerPrefs.SetInt(StatueWeaponCheck(), PlayerPrefs.GetInt("carriedWeapon"));
                    PlayerPrefs.SetInt("carriedWeapon", 0);
                    ChangeWeapons();
                }
                else if(PlayerPrefs.GetInt(StatueWeaponCheck()) > 0)
                {
                    PlayerPrefs.SetInt("carriedWeapon", PlayerPrefs.GetInt(StatueWeaponCheck()));
                    PlayerPrefs.SetInt(StatueWeaponCheck(), 0);
                    ChangeWeapons();
                }
            }

        }
    }

    void ChangeHeads()
    {
        if(PlayerPrefs.GetInt(StatueBodyCheck()) != statueManager.statues[statueElement].currentHead)
        {
            statueManager.statues[statueElement].currentHead = PlayerPrefs.GetInt(StatueBodyCheck());
        }

        bodySpriteRenderer.sprite = headlessBodySprite;

        if(statueManager.statues[statueElement].currentHead > statueManager.heads.Count || statueManager.statues[statueElement].currentHead <= 0)
        {
            headSpriteRenderer.sprite = null;
        }
        else
        {
            headSpriteRenderer.sprite = statueManager.heads[statueManager.statues[statueElement].currentHead - 1];
        }

    }

    public void ChangeWeapons()
    {
        if(!statueManager.HeadPuzzleComplete())
        {
            return;
        }

        if(PlayerPrefs.GetInt(StatueWeaponCheck()) != statueManager.statues[statueElement].currentWeapon)
        {
            statueManager.statues[statueElement].currentWeapon = PlayerPrefs.GetInt(StatueWeaponCheck());
        }

        if(statueManager.statues[statueElement].currentWeapon > weaponBodySprite.Count || statueManager.statues[statueElement].currentWeapon <= 0)
        {
            bodySpriteRenderer.sprite = headlessBodySprite;
            headSpriteRenderer.enabled = true;
        }
        else
        {
            bodySpriteRenderer.sprite = weaponBodySprite [statueManager.statues[statueElement].currentWeapon - 1];
            headSpriteRenderer.enabled = false;
        }

        statueManager.CheckWeaponPuzzleStatus();
    }

    string StatueBodyCheck()
    {
        return "statueBody" + statueElement;
    }


    string StatueWeaponCheck()
    {
        return "statueWeapon" + statueElement;
    }

}
