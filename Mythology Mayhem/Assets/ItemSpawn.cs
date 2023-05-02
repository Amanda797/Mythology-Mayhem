using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public List<TransItem> items = new List<TransItem>();
    public Transform itemSpawnPoint;
    public bool ResstPref;
    List<GameObject> spawnedItems = new List<GameObject>();
    bool hasSpawned;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(PlayerPrefs.HasKey(items[i].name))
            {
                spawnedItems.Add(Instantiate(items[i].item,JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString(items[i].name)),Quaternion.identity));
                //SaveScene.AddObject(Instantiate(items[i].item,JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString(items[i].name)),Quaternion.identity),items[i].item);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasSpawned)
        {
            if(ResstPref)
        {
            PlayerPrefs.DeleteKey("TranstionItem");
        }
        if(items.Count > 0)
        {
            if(PlayerPrefs.HasKey("TranstionItem"))
            {
                print(PlayerPrefs.GetString("TranstionItem"));
                string name = PlayerPrefs.GetString("TranstionItem");
                foreach(TransItem item in items)
                {
                    if(item.name == name)
                    {
                        
                        spawnedItems.Add(Instantiate(item.item,itemSpawnPoint.position,Quaternion.identity));
                        //SaveScene.AddObject(Instantiate(item.item,itemSpawnPoint.position,Quaternion.identity),item.item);
                        PlayerPrefs.DeleteKey("TranstionItem");
                        

                    }
                }
            }
        }
        hasSpawned = true;
        }

        if(spawnedItems.Count > 0)
        {
            foreach(GameObject item in spawnedItems)
            {
                if(item == null)
                {
                    spawnedItems.Remove(item);
                    continue;
                }
                PlayerPrefs.SetString(item.name, JsonUtility.ToJson(item.transform.position));
            }
        }
    }
}
[System.Serializable]
public class TransItem
{
    public string name;
    public GameObject item;
}
