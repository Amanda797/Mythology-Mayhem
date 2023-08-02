using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHeartPickup : MonoBehaviour
{
    bool collected = false;

    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;
    private Vector2 posOffset = new Vector2();
    private Vector2 tempPos = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
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
            other.GetComponent<PlayerStats>().huic.PlayerMaxHealth = other.GetComponent<PlayerStats>().huic.PlayerCurrHealth + 4;
            collected = true;
            gameObject.SetActive(false);
       }
    }//end on trigger enter 2d
}
