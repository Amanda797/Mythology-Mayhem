using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelectDetails : MonoBehaviour
{
    [SerializeField] GameObject LeftBorder;
    [SerializeField] GameObject RightBorder;
    float leftXScale;
    float rightXScale;
    [SerializeField] GameObject BackButton;
    [SerializeField] GameObject ConfirmButton;
    [SerializeField] GameObject [] characters;
    int selectedCharacter;
    int playerIndex;
    [SerializeField] GameObject [] characterButtons;
    [SerializeField] GameObject CharacterDescriptionPanel;
    [SerializeField] TextMeshProUGUI cdpText;
    [SerializeField] TextMeshProUGUI cdpHead;
    float descripPosX;
    int sceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        playerIndex = -1;
        //error fix
        print(playerIndex);
        leftXScale = 1f;
        rightXScale = 1f;
        descripPosX = 1f;
        LeftBorder.GetComponent<RectTransform>().localScale = new Vector3(leftXScale,850f,0);
        RightBorder.GetComponent<RectTransform>().localScale = new Vector3(rightXScale,850f,0);
        CharacterDescriptionPanel.GetComponent<RectTransform>().localPosition = new Vector3(descripPosX,-66f,0);
        BackButton.SetActive(false);
        ConfirmButton.SetActive(false);
        CharacterDescriptionPanel.SetActive(false);
    }//end start    
    
    public void SelectCharacter(int ch) {

        switch(ch) {
            case 0: {
                print("Selected Character 1");
                // Deactivate character buttons
                foreach(GameObject go in characters) {
                    go.SetActive(false);
                }
                selectedCharacter = ch;
                leftXScale = 400f;
                rightXScale = 2680f;
                BackButton.SetActive(true);
                ConfirmButton.SetActive(true);
                foreach(GameObject cb in characterButtons) {
                    cb.SetActive(false);
                }
                descripPosX = 10f;
                CharacterDescriptionPanel.SetActive(true);
                cdpHead.text = "TOBIAS";
                cdpText.text = "Tobias hails from Greece. He is a scribe in the Library of Alexandria. His special ability is more force behind his sword attacks.";
                break;
            }
            case 1: {
                print("Selected Character 2");
                // Deactivate character buttons
                foreach(GameObject go in characters) {
                    go.SetActive(false);
                }
                selectedCharacter = ch;
                leftXScale = 1100f;
                rightXScale = 1860f;
                BackButton.SetActive(true);
                ConfirmButton.SetActive(true);
                foreach(GameObject cb in characterButtons) {
                    cb.SetActive(false);
                }
                descripPosX = 380f;
                CharacterDescriptionPanel.SetActive(true);
                cdpHead.text = "GORM";
                cdpText.text = "Gorm comes from the icy north, in a Viking village. He was meant to be a great warrior and yields an axe, but his ability to protect himself and friends gives him a better defense.";
                break;
            }
            case 2: {
                print("Selected Character 3");
                // Deactivate character buttons
                foreach(GameObject go in characters) {
                    go.SetActive(false);
                }
                selectedCharacter = ch;
                leftXScale = 1860f;
                rightXScale = 1100f;
                BackButton.SetActive(true);
                ConfirmButton.SetActive(true);
                foreach(GameObject cb in characterButtons) {
                    cb.SetActive(false);
                }
                descripPosX = -390f;
                CharacterDescriptionPanel.SetActive(true);
                cdpHead.text = "AMUNET";
                cdpText.text = "Amunet resides in Egypt and grew up in the palace. She has more powerful magical attacks and can glide past enemies.";
                break;
            }
            case 3: {
                print("Selected Character 4");
                // Deactivate character buttons
                foreach(GameObject go in characters) {
                    go.SetActive(false);
                }
                selectedCharacter = ch;
                leftXScale = 2680f;
                rightXScale = 400f;
                BackButton.SetActive(true);
                ConfirmButton.SetActive(true);
                foreach(GameObject cb in characterButtons) {
                    cb.SetActive(false);
                }
                descripPosX = 10f;
                CharacterDescriptionPanel.SetActive(true);
                cdpHead.text = "MICOS";
                cdpText.text = "Micos is from a small Inca village in the deepest jungles of Peru. He has a better shooting attack with bow and arrow and he can jump really high.";
                break;
            }
            case 5: {
                // Confirm Selection
                // Save selectedCharacter
                if(selectedCharacter != -1) {
                    print("Selected Character is: " + characters[selectedCharacter].name);
                    PlayerPrefs.SetInt("playerIndex", selectedCharacter);
                    foreach(GameObject go in Object.FindObjectsOfType<GameObject>()) {
                        if(go != this) {
                            Destroy(go);
                        }
                    }

                    if(PlayerPrefs.HasKey("sceneIndex"))
                    {
                        sceneIndex = PlayerPrefs.GetInt("sceneIndex");                    
                        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
                    }
                    else
                    {
                        SceneManager.LoadScene("Cutscene1", LoadSceneMode.Single);
                    }
                }
                break;
            }
            case 6: {
                // Go Back to the Main Menu
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                break;
            }
            default: { 
                // Go Back
                // Reactivate character buttons
                foreach(GameObject go in characters) {
                    go.SetActive(true);
                }
                selectedCharacter = -1;
                playerIndex = -1;
                leftXScale = 1f;
                rightXScale = 1f;
                descripPosX = 1f;
                BackButton.SetActive(false);
                ConfirmButton.SetActive(false);
                foreach(GameObject cb in characterButtons) {
                    cb.SetActive(true);
                }
                CharacterDescriptionPanel.SetActive(false);
                break;
            }
        }
        
        LeftBorder.GetComponent<RectTransform>().localScale = new Vector3(leftXScale,850f,0);
        RightBorder.GetComponent<RectTransform>().localScale = new Vector3(rightXScale,850f,0);
        CharacterDescriptionPanel.GetComponent<RectTransform>().localPosition = new Vector3(descripPosX,-66f,0);
    }//end Select Character

}
