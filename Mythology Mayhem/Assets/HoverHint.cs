using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverHint : MonoBehaviour
{
    [SerializeField] GameObject hintBox;
    [SerializeField] TextMeshProUGUI textMesh;
    [TextArea(5,9)]
    [SerializeField] string hintText;

    // Start is called before the first frame update
    void Start()
    {
        hintBox.SetActive(false);
        textMesh.text = hintText;        
    }

    private void OnMouseEnter() {
        hintBox.SetActive(true);
    }//exit on mouse over

    private void OnMouseExit() {
        hintBox.SetActive(false);
    }
}
