using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StatueWeapon;

public class StatueBody2D : MonoBehaviour
{
    GameManager gameManager;
    public SpriteRenderer bodySpriteRenderer;
    public SpriteRenderer headSpriteRenderer;
    public SpriteRenderer weaponSpriteRenderer;
    public StatuePuzzle statueManager;
    private GameObject player;
    public float interactDistance;
    public int statueElement;

    public Sprite headlessBodySprite;

    [SerializeField] Sprite weaponBodySprite;
    [SerializeField] bool canPlace = false;
    [SerializeField] bool canPickup = false;
    public bool hasCorrectWeapon = false;

    public Weapon currentWeapon = Weapon.Null;
    public Weapon correctWeapon = Weapon.Null;
    GameObject currentWeaponObject;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickup && currentWeapon != Weapon.Null) if (Input.GetKeyUp(KeyCode.E)) PickUpWeapon();
        if (canPlace && !hasCorrectWeapon) if(Input.GetKeyUp(KeyCode.E)) if (currentWeapon == Weapon.Null) PlaceWeapon();     
    }

    //public void ChangeHeads()
    //{
    //    if(PlayerPrefs.GetInt(StatueBodyCheck()) != statueManager.statues[statueElement].currentHead)
    //    {
    //        statueManager.statues[statueElement].currentHead = PlayerPrefs.GetInt(StatueBodyCheck());
    //    }

    //    bodySpriteRenderer.sprite = headlessBodySprite;

    //    if(statueManager.statues[statueElement].currentHead > statueManager.heads.Count || statueManager.statues[statueElement].currentHead <= 0)
    //    {
    //        headSpriteRenderer.sprite = null;
    //    }
    //    else
    //    {
    //        headSpriteRenderer.sprite = statueManager.heads[statueManager.statues[statueElement].currentHead - 1];
    //    }

    //}

    //public void ChangeWeapons()
    //{
    //    if(!statueManager.HeadPuzzleComplete())
    //    {
    //        return;
    //    }

    //    if(PlayerPrefs.GetInt(StatueWeaponCheck()) != statueManager.statues[statueElement].currentWeapon)
    //    {
    //        statueManager.statues[statueElement].currentWeapon = PlayerPrefs.GetInt(StatueWeaponCheck());
    //    }

    //    if(statueManager.statues[statueElement].currentWeapon > weaponBodySprite.Count || statueManager.statues[statueElement].currentWeapon <= 0)
    //    {
    //        bodySpriteRenderer.sprite = headlessBodySprite;
    //        headSpriteRenderer.enabled = true;
    //    }
    //    else
    //    {
    //        bodySpriteRenderer.sprite = weaponBodySprite [statueManager.statues[statueElement].currentWeapon - 1];
    //        headSpriteRenderer.enabled = false;
    //    }

    //    statueManager.CheckWeaponPuzzleStatus();
    //}

    void PlaceWeapon()
    {
        GameObject weapon = statueManager.currentWeaponObject;
        weapon.transform.parent = this.gameObject.transform;
        weapon.transform.position = weaponSpriteRenderer.transform.position;
        currentWeapon = statueManager.currentWeapon;
        statueManager.currentWeapon = Weapon.Null;
        statueManager.currentWeaponObject.GetComponent<StatueWeapon>().inHand = false;
        currentWeaponObject = statueManager.currentWeaponObject;
        statueManager.currentWeaponObject = null;
        if (currentWeapon == correctWeapon)
        {
            hasCorrectWeapon = true;
            weaponSpriteRenderer.sprite = null;
            bodySpriteRenderer.sprite = weaponBodySprite;
            headSpriteRenderer.sprite = null;
            currentWeaponObject.SetActive(false);
            statueManager.CheckWeaponPuzzleStatus();
        }

        // TODO: need to do something with the weapon object after placing it.
    }

    void PickUpWeapon()
    {
        Debug.Log("PickUpWeapon");
        GameObject weapon = currentWeaponObject;
        weapon.transform.parent = player.transform;
        weapon.transform.position = player.transform.position;
        statueManager.currentWeapon = currentWeapon;
        currentWeapon = Weapon.Null;
        statueManager.currentWeaponObject = currentWeaponObject;
        statueManager.currentWeaponObject.GetComponent<StatueWeapon>().inHand = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (hasCorrectWeapon) return;
            player = other.gameObject;
            string message = "";

            // if the player is holding a weapon
            if (statueManager.currentWeapon != Weapon.Null)
            {
                // if the statue does not have a weapon
                if (currentWeapon == Weapon.Null)
                {
                    message = "Place " + statueManager.currentWeapon.ToString();
                    canPlace = true;
                }
                else message = "Drop " + statueManager.currentWeapon.ToString();
                gameManager.Popup(message, true);
            }
            else if (currentWeapon != Weapon.Null)
            {
                message = "Pickup " + currentWeapon.ToString();
                canPickup = true;
                gameManager.Popup(message, true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("", false);
            canPlace = false;
            canPickup = false;
        }
    }
}
