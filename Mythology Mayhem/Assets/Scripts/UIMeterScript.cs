using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMeterScript : MonoBehaviour
{
    // The meter's background transform
    public RectTransform background;
    // The meter's foreground transform (the image that should be scalable)
    public RectTransform foreground;
    // The width of the background's transform, as big as the background and foreground can get
    float maxMeter;
    // The width of the foreground's transform currently, its current percentage of fullness
    float currentMeter;
    // The width in terms of 100
    float currentMeterWeighted;
    public TextMeshProUGUI currentMeterNumber;
    
    int testStep = 1;
    float timer = 0f;
    float duration = 3f;

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
    }

    // Update is called once per frame
    void Update()
    {
        //Testing functionality. Unhide: testStep, timer, and duration.

        timer += Time.deltaTime;

        if(timer > duration) {
            switch(testStep) {
                case 1: DecreaseMeter(40); break;
                case 2: IncreaseMeter(20); break;
                case 3: DecreaseMeter(90); break;
                case 4: IncreaseMeter(100.9f); break;
                case 5: DecreaseMeter(150); break;
                case 6: IncreaseMeter(50); break;
                case 7: DecreaseMeter(-30); break;
                case 8: IncreaseMeter(-70); break;
                case 9: DecreaseMeter(13.38f); break;
                case 10: IncreaseMeter(45.9f); break;
                default: break;
            }
            testStep++; 
            timer = 0f;
        }
        

        // Constrain the meter to the mins and max's of the transforms
        if(currentMeter <= 0) {
            currentMeter = 0;
        } else if (currentMeter >= maxMeter) {
            currentMeter = maxMeter;
        }

        // reset foreground size according to updated value of currentMeter
        foreground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentMeter);
        foreground.ForceUpdateRectTransforms();

        // Set the text panel's number
        currentMeterWeighted = (currentMeter * 100) / maxMeter;
        currentMeterNumber.text = currentMeterWeighted + "";
    }

    public void IncreaseMeter(float amount) {
        float newLevel = currentMeter + ((Mathf.Abs(amount) * maxMeter) / 100);
        currentMeter = newLevel;
        // newLevel = {ex.3452} + ((amount * maxMeter) / 100);
    }

    public void DecreaseMeter(float amount) {
        
        float newLevel = currentMeter - ((Mathf.Abs(amount) * maxMeter) / 100);
        currentMeter = newLevel;
        // newLevel = {ex.3452} - ((amount * maxMeter) / 100);
    }

}
