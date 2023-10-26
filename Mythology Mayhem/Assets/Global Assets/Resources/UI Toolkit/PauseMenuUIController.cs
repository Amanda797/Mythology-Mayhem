using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Xml;
using TMPro;

public class PauseMenuUIController : MonoBehaviour
{
    [SerializeField] GameObject pauseBackground;
    [SerializeField] Sprite pauseSprite;
    [SerializeField] TextMeshProUGUI menuTitle;
    [SerializeField] GameObject returnButton;

    [Header("Help")]
    [SerializeField] GameObject HelpContainer;
    [SerializeField] GameObject HelpContent;
    [SerializeField] GameObject HelpPrefab;
    [SerializeField] HelpOption[] _help;


    private void OnEnable()
    {
        //Pause Background
        pauseBackground.GetComponent<Image>().image = pauseSprite.texture;

        //Help
        foreach(HelpOption help in _help)
        {
            GameObject newPrefab = Instantiate(HelpPrefab, HelpContent.transform);
            newPrefab.GetComponent<TextMeshProUGUI>().text = help.Description;
            if(help.IsTitle)
            {
                newPrefab.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    

    
}

[System.Serializable]
public class HelpOption
{
    public bool IsTitle;
    [TextArea(3,8)]
    public string Description;
}

[System.Serializable]
public class CreditOption
{
    public string Title;
    public string Creditor;
}
