using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private PlayerStats player;
    [SerializeField] private bool smallPotion;

    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;

    private Vector2 posOffset = new Vector2();
    private Vector2 tempPos = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == 3)
        {
            if (smallPotion) 
                other.gameObject.GetComponent<PlayerStats>().Heal(2);
            else
                other.gameObject.GetComponent<PlayerStats>().Heal(4);
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }  
    }
}
