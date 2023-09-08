using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPopup : MonoBehaviour
{
    public KeyCode key;
    public float activeDistance;
    public float hoverDistance;
    float hover;
    public GameObject popup;
    public GameObject pressText;
    public GameObject supriseObject;
    bool isPopupActive = false;
    GameObject player;
    bool surpriseSpawned = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hover = hoverDistance;
        StartCoroutine(Hover());
    }
    
    void Update()
    {
        //check if the player looking in the direction of the scroll


        if(Vector3.Distance(player.transform.position, transform.position) < activeDistance && Vector3.Angle(player.transform.forward, transform.position - player.transform.position) < 45f)
        {
            if(!isPopupActive)
            {
                pressText.SetActive(true);
            }
            else
            {
                pressText.SetActive(false);
            }
            if (Input.GetKeyDown(key))
            {
                if (isPopupActive)
                {
                    HidePopup();
                    isPopupActive = false;
                }
                else
                {
                    ShowPopup();
                    //StopCoroutine(Hover());
                    isPopupActive = true;
                    if(supriseObject != null && !surpriseSpawned)
                        supriseObject.SetActive(true);
                        surpriseSpawned = true;
                }
            }
        }
        else
        {
            pressText.SetActive(false);
        }
        
    }
    public void ShowPopup()
    {
        popup.SetActive(true);
        hover = 0;
        Time.timeScale = 0;
    }

    public void HidePopup()
    {
        popup.SetActive(false);
        hover = hoverDistance;
        Time.timeScale = 1;
    }

    // Ienumerator that makes the scroll hover up and down with lerping
    IEnumerator Hover()
    {
        while (true)
        {
            float t = 0f;
            while (t <= 0.9f)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * hover, t);
                yield return null;
            }
            t = 0f;
            while (t <= 0.9f)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, transform.position - Vector3.up * hover, t);
                yield return null;
            }
        }
    }
}
