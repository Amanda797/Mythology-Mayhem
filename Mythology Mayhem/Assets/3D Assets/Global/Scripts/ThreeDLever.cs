using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreeDLever : MonoBehaviour
{
    GameManager gameManager;

    //[SerializeField] private string nextLevel = "Library 3D";
    [SerializeField] private BoxCollider DoorTrigger;
    bool canOpen = false;

    ChangeNextSpawn CNS;
    
    // Start is called before the first frame update
    void Start()
    {
        // try to find the GameManager object
        if (GameManager.instance != null) gameManager = GameManager.instance;
        // else display a warning that it is missing
        else Debug.LogWarning("GameManager Missing or Inactive.");

        //CNS = GetComponent<ChangeNextSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canOpen) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!DoorTrigger.enabled) DoorTrigger.enabled = true;
            //CNS.NextSpawn(2);
            //LoadNextScene();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Pull Lever", true);

            canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.Popup("Press E to Pull Lever", false);

            canOpen = false;
        }
    }
    //public void LoadNextScene()
    //{
    //    SceneManager.LoadScene(nextLevel);
    //}
}
