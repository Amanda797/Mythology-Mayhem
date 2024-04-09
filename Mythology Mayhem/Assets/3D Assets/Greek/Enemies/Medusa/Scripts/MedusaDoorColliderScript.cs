using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MedusaDoorColliderScript : MonoBehaviour
{
    public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            print("Player in Doorway");
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                PlayerPrefs.SetString("spawningScene", nextScene);
                string loadScene = PlayerPrefs.GetString("spawningScene");
                SceneManager.LoadScene(loadScene);
            }
        }
    }
}
