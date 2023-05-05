using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCharacter : MonoBehaviour
{
    [SerializeField] int characterIndex;
    [SerializeField] CharacterSelectDetails csd;
    void OnMouseDown() {
        csd.SelectCharacter(characterIndex);
    }//end OnMouseDown
}
