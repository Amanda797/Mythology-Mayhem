using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMeterScript : MonoBehaviour
{
    // The meter's background transform
    public RectTransform background;
    // The meter's foreground transform (the image that should be scalable)
    public RectTransform foreground;
    // The width of the background's transform, as big as the background and foreground can get
    float maxMeter;
    // The width of the foreground's transform currently, its current percentage of fullness
    public float currentMeter;

    // testing variables, timer to track passing time, duration to set off alarm, ten to reduce the meter's "fullness" from 0 to 100
    public float timer = 0f;
    public float duration = 10f;
    float ten = 100;    

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
        // count passing seconds
        timer += Time.deltaTime;


        // adjust meter alarm
        if(timer > duration) {
            AdjustMeter(ten);
            ten -= 10;
            timer = 0f;
        }

        // reset foreground size according to updated value of currentMeter
        foreground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentMeter);
        foreground.ForceUpdateRectTransforms();
    }

    // newLevel on a scale of 0 - 100 
    public void AdjustMeter(float newLevelTen) {
        if(newLevelTen >= 0) {
            print(currentMeter);
            float newLevel = (maxMeter * newLevelTen) / 100;
            print(newLevel + ":" + maxMeter + "*" + newLevelTen + " / 100");
            currentMeter = newLevel;
            print(currentMeter);
        }
        
    }
}
