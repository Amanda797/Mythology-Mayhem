using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollControl : MonoBehaviour
{
    [SerializeField] float multiplier = 2f;
    [SerializeField] Scrollbar scrollbarValue;
    [SerializeField] Vector2 mouseWheelInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseWheelInput = Input.mouseScrollDelta;

        if(mouseWheelInput.y != 0) {
            scrollbarValue.value += mouseWheelInput.y * multiplier;
        }
    }
}
