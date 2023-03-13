using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCode : MonoBehaviour
{
    [SerializeField] private string nextLevel = "Library 3D 2";
    public bool entered = false;
    public bool blocked = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   void Update()
    {
        if (entered && !blocked)
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                LoadNextScene();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == 3) 
        {
            entered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.layer == 3) 
        {
            entered = false;
        }
        if (other.tag == "PushBlock")
        {
            blocked = false;
        }
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
