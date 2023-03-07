using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoDLever : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private string nextLevel = "Library 3D";

    public bool entered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (entered)
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                anim.SetTrigger("Pulled");
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
        
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
