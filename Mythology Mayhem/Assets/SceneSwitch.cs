using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public string sceneName;
    public float distance;
    Transform player;
    itemSlot itemSlot;
    //SaveScene saveScene;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        itemSlot = FindObjectOfType<itemSlot>();
        //saveScene = GameObject.FindGameObjectWithTag("SaveScene").GetComponent<SaveScene>();
    }

    void Update()
    {
        if(Vector3.Distance(player.position, transform.position) < distance)
        {
            print("Right Disatnce: " + itemSlot.gameObject.activeSelf);
            if(itemSlot.item != null)
            {
                if(itemSlot.gameObject != null)
                {
                    // foreach (var item in saveScene.sceneObjects.objects)
                    // {
                    //     if(item.gameObject == itemSlot.gameObject)
                    //     {
                    //         item.isEnabled = false;
                    //     }
                    // }
                    // saveScene.sceneObjects.objects[IndexOfScene(itemSlot.gameObject)].isEnabled = false;
                    // //Destroy(itemSlot.gameObject);
                    // itemSlot.NotSave();

                    PlayerPrefs.SetString("TranstionItem", itemSlot.item.name);
                    print(PlayerPrefs.GetString("TranstionItem"));

                    SceneManager.LoadScene(sceneName);  
                }
            }
            
        }
    }

    public int IndexOfScene(GameObject gameObject)
    {
        int index = 0;
        // foreach (var item in saveScene.sceneObjects.objects)
        // {
        //     if(item.gameObject == gameObject)
        //     {
        //         index = saveScene.sceneObjects.objects.IndexOf(item);
        //     }
        // }
        return index;
    }
    void LateUpdate()
    {
        
    }
}
