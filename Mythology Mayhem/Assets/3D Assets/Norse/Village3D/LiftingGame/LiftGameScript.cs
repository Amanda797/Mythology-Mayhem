using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiftGameScript : MonoBehaviour
{
    public LiftVersion currentLift;

    public Animator anim;

    public float liftAmount;
    public float amountPerPress;
    public float drainRate;

    public GameObject indicator;

    public Vector2[] liftAndDrain;
    public Vector2[] indicatorMinMax;

    public Image[] liftBar;
    public LiftAnimScript animScript;

    public LiftVersion testLift;

    public enum LiftVersion 
    { 
        Ten,
        Thirty,
        Sixty,
        Hundred
    }

    private void Start()
    {
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            liftAmount += amountPerPress;
        }

        liftAmount -= Time.deltaTime * drainRate;
        if (liftAmount < 0)
        {
            liftAmount = 0;
        }

        if (liftAmount > 100)
        {
            liftAmount = 100;
        }
        float percentage = liftAmount / 100;
        switch (currentLift)
        {
            case LiftVersion.Ten:
                indicator.transform.localPosition = new Vector3(Mathf.Lerp(indicatorMinMax[0].x, indicatorMinMax[0].y, percentage), 15, 0);
                break;
            case LiftVersion.Thirty:
                indicator.transform.localPosition = new Vector3(Mathf.Lerp(indicatorMinMax[1].x, indicatorMinMax[1].y, percentage), 15, 0);
                break;
            case LiftVersion.Sixty:
                indicator.transform.localPosition = new Vector3(Mathf.Lerp(indicatorMinMax[2].x, indicatorMinMax[2].y, percentage), 15, 0);
                break;
            case LiftVersion.Hundred:
                indicator.transform.localPosition = new Vector3(Mathf.Lerp(indicatorMinMax[3].x, indicatorMinMax[3].y, percentage), 15, 0);
                break;
        }

        if (Input.GetKeyDown(KeyCode.Y)) 
        {
            SetLift(testLift);
        }
    }

    void SetLift(LiftVersion total) 
    {
        currentLift = testLift;
        switch (total)
        {
            case LiftVersion.Ten:
                amountPerPress = liftAndDrain[0].x;
                drainRate = liftAndDrain[0].y;
                ClearPowerBar();
                liftBar[0].gameObject.SetActive(true);
                animScript.SetRock(0);
                break;
            case LiftVersion.Thirty:
                amountPerPress = liftAndDrain[1].x;
                drainRate = liftAndDrain[1].y;
                ClearPowerBar();
                liftBar[0].gameObject.SetActive(true);
                liftBar[1].gameObject.SetActive(true);
                liftBar[2].gameObject.SetActive(true);
                animScript.SetRock(1);
                break;
            case LiftVersion.Sixty:
                amountPerPress = liftAndDrain[2].x;
                drainRate = liftAndDrain[2].y;
                ClearPowerBar();
                liftBar[0].gameObject.SetActive(true);
                liftBar[1].gameObject.SetActive(true);
                liftBar[2].gameObject.SetActive(true);
                liftBar[3].gameObject.SetActive(true);
                liftBar[4].gameObject.SetActive(true);
                liftBar[5].gameObject.SetActive(true);
                animScript.SetRock(2);
                break;
            case LiftVersion.Hundred:
                amountPerPress = liftAndDrain[3].x;
                drainRate = liftAndDrain[3].y;
                ClearPowerBar();
                liftBar[0].gameObject.SetActive(true);
                liftBar[1].gameObject.SetActive(true);
                liftBar[2].gameObject.SetActive(true);
                liftBar[3].gameObject.SetActive(true);
                liftBar[4].gameObject.SetActive(true);
                liftBar[5].gameObject.SetActive(true);
                liftBar[6].gameObject.SetActive(true);
                liftBar[7].gameObject.SetActive(true);
                liftBar[8].gameObject.SetActive(true);
                liftBar[9].gameObject.SetActive(true);
                animScript.SetRock(3);
                break;
        }
    }

    void ClearPowerBar() 
    {
        foreach (Image img in liftBar) 
        {
            img.gameObject.SetActive(false);
        }
    }
}
