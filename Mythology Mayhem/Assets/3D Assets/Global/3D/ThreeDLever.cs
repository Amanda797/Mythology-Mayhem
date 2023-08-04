using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreeDLever : MonoBehaviour
{
    [SerializeField] private string nextLevel = "Library 3D";
    [SerializeField] private BoxCollider trigger;
    public bool entered = false;

    ChangeNextSpawn CNS;
    
    // Start is called before the first frame update
    void Start()
    {
        CNS = GetComponent<ChangeNextSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (entered)
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                CNS.NextSpawn(2);
                LoadNextScene();
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            entered = true;
        }    
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Player")
        {
            entered = false;
        }    
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
