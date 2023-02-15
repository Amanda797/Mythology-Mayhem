using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMeterScript : MonoBehaviour
{
    // The meter's background transform
    [SerializeField] RectTransform background;
    // The meter's foreground transform (the image that should be scalable)
    [SerializeField] RectTransform foreground;
    // The width of the background's transform, as big as the background and foreground can get
    float maxMeter;
    // The width of the foreground's transform currently, its current percentage of fullness
    float currentMeter;
    // The base that the meter is set in (ex 100/100, playerCurrHealth/playerMaxHealth, etc.)
    float maxBase;
    // The width in terms of maxBase
    float currentMeterWeighted;
    [SerializeField] TextMeshProUGUI currentMeterNumber;
    
    // Testing variables
    //int testStep = 1;
    //float timer = 0f;
    //float duration = 3f;

    // Start is called before the first frame update
    void Start()
    {
        // set maxMeter equal to the background's rect transform width
        maxMeter = background.rect.width;
        print(maxMeter);

        // set the size (width) of the foreground's rect transform on the horizontal axis using the starting width, maxMeter
        foreground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxMeter);
        foreground.ForceUpdateRectTransforms();

        // set the scalable meter, currentMeter, equal to the starting width, maxMeter
        currentMeter = maxMeter;

        // set max base to a default until the scale is set by player ui
        maxBase = 100;
    }

    // Update is called once per frame
    void Update()
    {
        //Testing functionality. Unhide: testStep, timer, and duration.
        /*
        timer += Time.deltaTime;
        
        if(timer > duration) {
            switch(testStep) {
                case 1: UpdateMeter(40); break;
                case 2: UpdateMeter(20); break;
                case 3: UpdateMeter(90); break;
                case 4: UpdateMeter(100.9f); break;
                case 5: UpdateMeter(150); break;
                case 6: UpdateMeter(50); break;
                case 7: UpdateMeter(-30); break;
                case 8: UpdateMeter(-70); break;
                case 9: UpdateMeter(13.38f); break;
                case 10: UpdateMeter(45.9f); break;
                default: break;
            }
            testStep++; 
            timer = 0f;
        }
        */
        
        // Constrain the meter to the mins and max's of the transforms
        if(currentMeter <= 0) {
            currentMeter = 0;
        } else if (currentMeter >= maxMeter) {
            currentMeter = maxMeter;
        }

        // Reset the foreground size according to the updated value of currentMeter
        foreground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentMeter);
        foreground.ForceUpdateRectTransforms();

        // Set the text panel's number
        currentMeterWeighted = (currentMeter * maxBase) / maxMeter;
        currentMeterNumber.text = currentMeterWeighted + "";
    }

    

    // Update Meter updates the meter's current level to a new amount (that is weighted).
    public void UpdateMeter(float amount) {
        currentMeter = ((Mathf.Abs(amount) * maxMeter) / maxBase);
    }

    // Set Meter updates the meter's scale based on the maximum value possible and the current amount of that possibility used.
    // setMax - maximum limit
    // setCurr - current amount
    public void SetMeter(float setMax, float setCurr) {
        maxBase = setMax;
        currentMeter = ((Mathf.Abs(setCurr) * maxMeter) / maxBase);
    }
}
