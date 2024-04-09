using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] public string sceneName;
    public float distance;
    //public SaveScene saveScene;
    [SerializeField] Transform player;
    itemSlot itemSlot;
    //SaveScene saveScene;
    
    void Start()
    {
        itemSlot = FindObjectOfType<itemSlot>();
        //saveScene = GameObject.FindGameObjectWithTag("SaveScene").GetComponent<SaveScene>();
    }

    void Update()
    {
        if(player == null) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //print(Vector3.Distance(player.position, transform.position) < distance);

        if(Input.GetKeyDown(KeyCode.E) && Vector3.Distance(player.position, transform.position) < distance) {
            //PlayerPrefs.SetInt("spwanPointIndex", spawnPointIndex);

            SceneManager.LoadScene(sceneName, LoadSceneMode.Single); 
        }

        /*if(Vector3.Distance(player.position, transform.position) < distance)
        {
            //print("Right Distance: " + itemSlot.gameObject.activeSelf);
            if(itemSlot != null && itemSlot.item != null)
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
                    //print(PlayerPrefs.GetString("TranstionItem"));

                    SceneManager.LoadScene(sceneName, LoadSceneMode.Single);  
                }
            }
            
        }*/
    }

    public void LoadScene() {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single); 
    }

}
