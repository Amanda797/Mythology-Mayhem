using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemSlot : MonoBehaviour
{
    public Item item;
    public KeyCode key;
    public Image icon;
    public Transform player;
    bool SaveItem = true;
    public bool LoadItem;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("itemSlot") && LoadItem)
        {
            Load();
        }
        else
        {
            PlayerPrefs.DeleteKey("itemSlot");
            Save();
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKeyDown(key))
        {
            
            if(item.item != null)
            {
                //spwan the item in front of player
                item.item.transform.position = player.position + player.forward * 5;
                item.item.SetActive(true);
                item.item = null;
            }
            else
            {
                //shot a raycast to check if there is a object in front of player that has the tage "Item"
                RaycastHit hit;
                if(Physics.Raycast(player.position, player.forward, out hit, Mathf.Infinity))
                {
                    if(hit.collider.tag == "Item")
                    {
                        //if there is an item, set the item slot to the item
                        item.icon = hit.collider.GetComponent<ItemPickup>().item.icon;
                        item.item = hit.collider.GetComponent<ItemPickup>().item.item;
                        item.name = hit.collider.GetComponent<ItemPickup>().item.name;
                        icon.sprite = item.icon;
                        //Destroy(hit.collider.gameObject);
                        hit.collider.gameObject.SetActive(false);
                        //return;
                    }
                }
            }
        }

        if(item.item == null)
        {
            icon.enabled = false;
        }
        else
        {
            icon.enabled = true;
        }

        
    }

    public void Save()
    {
        
        if(SaveItem) PlayerPrefs.SetString("itemSlot", JsonUtility.ToJson(item));
    }
    public void NotSave()
    {
        SaveItem = false;
        PlayerPrefs.DeleteKey("itemSlot");
    }
    public void Load()
    {
        if(!PlayerPrefs.HasKey("itemSlot")) return;
        
        item = JsonUtility.FromJson<Item>(PlayerPrefs.GetString("itemSlot"));
        icon.sprite = item.icon;
    }

    void OnDestroy()
    {
        Save();
    }
}
[System.Serializable]
public class Item
{
    public string name;
    public Sprite icon;
    public GameObject item;
}
