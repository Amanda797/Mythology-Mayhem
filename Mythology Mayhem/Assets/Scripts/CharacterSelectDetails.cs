using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectDetails : MonoBehaviour
{
    [SerializeField] GameObject LeftBorder;
    [SerializeField] GameObject RightBorder;
    float leftXScale;
    float rightXScale;
    [SerializeField] GameObject BackButton;
    [SerializeField] GameObject ConfirmButton;
    [SerializeField] GameObject [] characters;
    [SerializeField] GameObject selectedCharacter;


    // Start is called before the first frame update
    void Start()
    {
        leftXScale = 1f;
        rightXScale = 1f;
        LeftBorder.GetComponent<RectTransform>().localScale = new Vector3(leftXScale,850f,0);
        RightBorder.GetComponent<RectTransform>().localScale = new Vector3(rightXScale,850f,0);
        BackButton.SetActive(false);
        ConfirmButton.SetActive(false);
    }//end start

    
    public void SelectCharacter(int ch) {

        switch(ch) {
            case 1: {
                print("Selected Character 1");
                // Deactivate character buttons
                foreach(GameObject go in characters) {
                    go.SetActive(false);
                }
                selectedCharacter = characters[ch-1];
                leftXScale = 400f;
                rightXScale = 2680f;
                BackButton.SetActive(true);
                ConfirmButton.SetActive(true);
                break;
            }
            case 2: {
                print("Selected Character 2");
                // Deactivate character buttons
                foreach(GameObject go in characters) {
                    go.SetActive(false);
                }
                selectedCharacter = characters[ch-1];
                leftXScale = 1100f;
                rightXScale = 1860f;
                BackButton.SetActive(true);
                ConfirmButton.SetActive(true);
                break;
            }
            case 3: {
                print("Selected Character 3");
                // Deactivate character buttons
                foreach(GameObject go in characters) {
                    go.SetActive(false);
                }
                selectedCharacter = characters[ch-1];
                leftXScale = 1860f;
                rightXScale = 1100f;
                BackButton.SetActive(true);
                ConfirmButton.SetActive(true);
                break;
            }
            case 4: {
                print("Selected Character 4");
                // Deactivate character buttons
                foreach(GameObject go in characters) {
                    go.SetActive(false);
                }
                selectedCharacter = characters[ch-1];
                leftXScale = 2680f;
                rightXScale = 400f;
                BackButton.SetActive(true);
                ConfirmButton.SetActive(true);
                break;
            }
            case 5: {
                // Confirm Selection
                // Save selectedCharacter
                if(selectedCharacter != null) {
                    print("Selected Character is: " + selectedCharacter.name);
                    //save prefab in playerprefs
                    //go to next scene
                }
                break;
            }
            default: { 
                // Go Back
                // Reactivate character buttons
                foreach(GameObject go in characters) {
                    go.SetActive(true);
                }
                selectedCharacter = null;
                leftXScale = 1f;
                rightXScale = 1f;
                BackButton.SetActive(false);
                ConfirmButton.SetActive(false);
                break;
            }
        }
        LeftBorder.GetComponent<RectTransform>().localScale = new Vector3(leftXScale,850f,0);
        RightBorder.GetComponent<RectTransform>().localScale = new Vector3(rightXScale,850f,0);
    }//end Select Character

}
