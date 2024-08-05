using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreeDLever : MonoBehaviour
{
    GameManager gameManager;
    AudioSource audioSource;
    [SerializeField] Animator animator;
    [SerializeField] private BoxCollider DoorTrigger;
    bool canOpen = false;
    bool isDown = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator.SetBool("isDown", false);
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
            animator.SetBool("isDown", !animator.GetBool("isDown"));
            audioSource.Play();
            DoorTrigger.enabled = animator.GetBool("isDown");
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
