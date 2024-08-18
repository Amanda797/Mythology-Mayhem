using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Mouse2D : MonoBehaviour
{
    [Header("Drop Scrolls")]
    [SerializeField] bool dropsScrolls;
    [SerializeField] float countdown = 4f;
    [SerializeField] GameObject fallingScroll;

    public void DropScrolls()
    {
        if (dropsScrolls)
        {
            if (countdown < 0)
            {
                countdown = Random.Range(3, countdown);
                Instantiate(fallingScroll, transform.position, Quaternion.identity);
            }
            else countdown -= 1 * Time.deltaTime;
        }
    }
}
