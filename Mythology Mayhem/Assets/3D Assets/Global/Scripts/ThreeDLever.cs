using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreeDLever : MonoBehaviour
{
    GameManager gameManager;
    AudioSource audioSource;
    [SerializeField] private BoxCollider DoorTrigger;
    bool canOpen = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing or Inactive.");
    }

    void Update()
    {
        if (!canOpen) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!DoorTrigger.enabled)
            {
                audioSource.Play();
                DoorTrigger.enabled = true;
            }
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
