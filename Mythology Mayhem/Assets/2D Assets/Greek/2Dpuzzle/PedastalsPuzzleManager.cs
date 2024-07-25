using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedastalsPuzzleManager : MonoBehaviour
{
    GameManager gameManager;
    AudioSource audioSource;
    public bool fish;
    public bool apple;
    public bool torch;
    public bool air;

    public bool fishDone;
    public bool appleDone;
    public bool torchDone;
    public bool airDone;

    public GameObject itemBow;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");
    }
    public void UpdateDoor()
    {
        if (fishDone && appleDone && torchDone && airDone)
        {
            audioSource.Play();
            if(!gameManager.gameData.collectedBow)
            {
                if(itemBow == null) itemBow = FindObjectOfType<TwoDBow>().gameObject;

                itemBow.SetActive(true);
            }
        }
    }

}
