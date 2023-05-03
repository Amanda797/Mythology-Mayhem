using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterSelectMovement : MonoBehaviour
{

    [SerializeField] Animator controller;
    [SerializeField] string[] animations;
    [SerializeField]float moveCounter;
    [SerializeField]int animIndex;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Animator>();
        moveCounter = 10f;
        animIndex = 0;

        controller.Play(animations[animIndex]);
    }//end start

    // Update is called once per frame
    void Update()
    {
        moveCounter -= 1f * Time.deltaTime;

        if(moveCounter <= 0) {
            ChangeAnimation();
            moveCounter = 10f;
        }

        controller.Play(animations[animIndex]); 
    }//end update

    void ChangeAnimation() {
        if(animIndex < animations.Length) {
            animIndex++;
        } else {
            animIndex = 0;
        }       
    }//end Change Animation

}// end CharacterSelectMovement
