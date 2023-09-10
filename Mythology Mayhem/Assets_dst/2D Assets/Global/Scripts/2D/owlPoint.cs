using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class owlPoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (sr.flipX)
        {
            if (player.GetComponent<PlayerStats>().flipped == false)
            {
                gameObject.transform.position = gameObject.transform.position * new Vector2(-1, 1);
            }
        }
        else
        {
            if (player.GetComponent<PlayerStats>().flipped == true)
            {
                gameObject.transform.position = gameObject.transform.position * new Vector2(-1, 1);
            }
        }
    }
}
