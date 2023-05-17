using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField] private Animator leverAnim;

    public bool inRange = false;
    public bool playAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange == true && Input.GetKey(KeyCode.E))
        {
            playAnim = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3) 
        {
            inRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer  == 3)
        {
            inRange = false;
        }
    }

}
