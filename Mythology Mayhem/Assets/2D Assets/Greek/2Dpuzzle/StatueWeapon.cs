using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueWeapon : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] StatuePuzzle statueManager;
    [SerializeField] GameObject player;
    [SerializeField] bool isPlaced = false;
    [SerializeField] bool canPickUp = false;
    public bool inHand = false;
    [SerializeField] bool nearStatue = false;
    [SerializeField] bool nearWeapon = false;
    public enum Weapon
    {
        Shield,
        Bow,
        Hammer,
        Sword,
        Spear,
        Null
    }
    public Weapon weapon = Weapon.Bow;

    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");
    }
    void Update()
    {
        if (inHand && !nearStatue) if (Input.GetKeyUp(KeyCode.E)) DropWeapon();
        if (canPickUp && !isPlaced) if (Input.GetKeyUp(KeyCode.E)) PickUPWeapon();
    }
    void PickUPWeapon()
    {
        transform.parent = player.transform;
        statueManager.currentWeapon = weapon;
        statueManager.currentWeaponObject = this.gameObject;
        inHand = true;
    }

    void DropWeapon()
    {
        if (nearWeapon) return;
        transform.parent = null;
        statueManager.currentWeapon = Weapon.Null;
        statueManager.currentWeaponObject = null;
        inHand = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            string popupMessage = "";

            // if the player is not holding a weapon
            if (statueManager.currentWeapon == Weapon.Null)
            {
                popupMessage = "Pickup " + weapon.ToString();
                canPickUp = true;
            }
            else
            {
                popupMessage = "Place or Drop " + statueManager.currentWeapon.ToString() + " first.";
                canPickUp = false;
            }
            gameManager.Popup(popupMessage, true);
        }
        else if (other.gameObject.tag == "StatueBody") nearStatue = true;
        else if (other.gameObject.tag == "StatueWeapon") nearWeapon = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("", false);
            canPickUp = false;
        }
        else if (other.gameObject.tag == "StatueBody") nearStatue = false;
        else if (other.gameObject.tag == "StatueWeapon") nearWeapon = false;
    }
}