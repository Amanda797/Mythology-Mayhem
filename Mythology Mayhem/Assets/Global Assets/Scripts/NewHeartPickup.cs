using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHeartPickup : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] AudioClip clip;
    bool collected = false;

    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;
    private Vector2 posOffset = new Vector2();
    private Vector2 tempPos = new Vector2();

    void Start()
    {
        posOffset = transform.position;
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing");
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D other) {
       if(!collected && other.tag == "Player") {
            AudioSource source = gameManager.GetComponent<AudioSource>();
            source.clip = clip;
            source.Play();
            other.GetComponent<PlayerStats>().CollectHeart(4);
            collected = true;
            gameObject.SetActive(false);
       }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!collected && other.tag == "Player")
        {
            AudioSource source = gameManager.GetComponent<AudioSource>();
            source.clip = clip;
            source.Play();
            gameManager.gameData.maxHealth += 4;
            gameManager.gameData.curHealth += 4;
            gameManager.huic.UpdateHealth();
            collected = true;
            gameObject.SetActive(false);
        }
    }
}
