using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Mouse2D : MonoBehaviour
{
    [Header("Drop Scrolls")]
    [SerializeField] float cooldown = 4f;
    [SerializeField] bool canDrop = true;
    [SerializeField] GameObject fallingScroll;

    public void DropScrolls()
    {
        if (!canDrop) return;
        canDrop = false;
        Instantiate(fallingScroll, transform.position, Quaternion.identity);
        StartCoroutine(Cooldown());
    }
    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canDrop = true;
    }
}
