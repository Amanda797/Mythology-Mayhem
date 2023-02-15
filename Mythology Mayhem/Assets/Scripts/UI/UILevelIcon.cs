using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelIcon : MonoBehaviour
{
        public RectTransform iconTransform;
        public Image iconSprite;

    // Start is called before the first frame update
    void Awake()
    {
        // Sets the icon into a square shape based on the height, which should be lined up based on the two meters' heights.
        float initHeight = iconTransform.rect.height;
        iconTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, initHeight);
        iconTransform.ForceUpdateRectTransforms();

        ChangeIcon(0);
    }

    public void ChangeIcon(int sceneIndex) {
        print("change icon: " + sceneIndex);

        switch(sceneIndex) {
            // All Level 1 Scenes
            /*
                1: "Library of Alexandria" (2D)
                2: "Library of Alexandria 3D"
                3: "City of Athens"
                4: etc.
            */

            case 1: case 2: case 3:
            {            
                iconSprite.sprite = Resources.Load<Sprite>("Background"); 
                iconSprite.color = Color.yellow; 
                break;
            }            
            // All Level 2 Scenes
            // All Level 3 Scenes
            // All Level 4 Scenes
            default: iconSprite.sprite = Resources.Load<Sprite>("Background"); iconSprite.color = Color.blue; break;
        }
    }
}
