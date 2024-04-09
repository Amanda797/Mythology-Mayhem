using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCode : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    [SerializeField] private GameObject lever;
    public bool entered = false;
    public bool blocked = true;
    public bool doorOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        if(nextLevel == "Athens" && gameObject.scene.ToString() == "2Dlabyrinth_Pedastals")
        {
            PedastalsPuzzleManager puzzleManager = FindObjectOfType<PedastalsPuzzleManager>();
            puzzleManager.door = this.gameObject;
            
            if (puzzleManager.fishDone && puzzleManager.appleDone && puzzleManager.torchDone && puzzleManager.airDone)
            {
                doorOpen = true;
                blocked = false;
            }
            else
            {
                doorOpen = false;
                blocked = true;
            }
        }
    }

    // Update is called once per frame
   void Update()
    {
        if (entered && !blocked && doorOpen)
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                LoadNextScene();
            }
        } else if(entered && !blocked && lever == null) {
            doorOpen = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == 3) 
        {
            entered = true;
        }
        if (other.tag == "PushBlock")
        {
            blocked = true;
            lever.GetComponent<TwoDLever>().SetCanOpen(false);
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
            lever.GetComponent<TwoDLever>().SetCanOpen(true);
        }
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }
    public void OpenDoor()
    {
        doorOpen = true;
    }
}
