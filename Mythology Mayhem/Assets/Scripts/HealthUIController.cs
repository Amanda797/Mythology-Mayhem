using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUIController : MonoBehaviour
{
    //UI Components
    [Header("UI Components")]
    private UIDocument _doc;
    private VisualElement _heartPanel;
    [SerializeField] private VisualTreeAsset _heartUXML;
    [SerializeField] private VisualElement _heartPrefab;
    [SerializeField] private List<Sprite> _heartModes;

    //External Components
    [Header("External Components")]
    [SerializeField] public PlayerStats_SO ps;

    //Properties
    public int PlayerCurrHealth{
        get {
           return ps.CurrHealth; 
        }
        set {
            ps.CurrHealth = Mathf.Clamp(value, 0, PlayerMaxHealth);
            SetHealthBar(PlayerCurrHealth);
            UpdateHealthBarCount(PlayerMaxHealth);
        }
    }

    public int PlayerMaxHealth{
        get {
            return ps.MaxHealth;
        }
        set {
            ps.MaxHealth = Mathf.Max(0, value);
            UpdateHealthBarCount(PlayerMaxHealth);
        }
    }

    void OnEnable() {
        _doc = GetComponent<UIDocument>();
        _heartPanel = _doc.rootVisualElement.Q("PlayerHealth");
        //Works in Build with a Resource Folder in Assets
        _heartUXML = Resources.Load<VisualTreeAsset>("UI Toolkit/Heart-UIBP.uxml");
        //Works in Editor
        _heartUXML = UnityEditor.AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Resources/UI Toolkit/Heart-UIBP.uxml");
        _heartPrefab = _heartUXML.CloneTree("Heart");

        _heartPanel.Add(_heartPrefab);
    }

    void Start() {
        UpdateHealthBarCount(PlayerMaxHealth);
        SetHealthBar(PlayerCurrHealth);
    }

    private void SetHealthBar(int health) {
        for (int i = 0; i < _heartPanel.childCount; i++)
        {
            int remainderHealth = Mathf.Clamp(health - (i * 4), 0, 4);
    
            switch (remainderHealth)
            {
                case 0:
                    _heartPanel.ElementAt(i).Q<VisualElement>("Heart").style.backgroundImage = new StyleBackground(_heartModes[4]);
                    break;
                case 1:
                    _heartPanel.ElementAt(i).Q<VisualElement>("Heart").style.backgroundImage = new StyleBackground(_heartModes[3]);
                    break;
                case 2:
                    _heartPanel.ElementAt(i).Q<VisualElement>("Heart").style.backgroundImage = new StyleBackground(_heartModes[2]);
                    break;
                case 3:
                    _heartPanel.ElementAt(i).Q<VisualElement>("Heart").style.backgroundImage = new StyleBackground(_heartModes[1]);
                    break;
                case 4:
                    _heartPanel.ElementAt(i).Q<VisualElement>("Heart").style.backgroundImage = new StyleBackground(_heartModes[0]);
                    break;
            };
        }
    }// end set health bar

    //Used to update the amount of Heart sprites in the Health Bar automatically.
    public void UpdateHealthBarCount(int maxHealth)
    {
        //Convert maxHealth "quarter hearts" value into whole hearts value.
        //Ceil to ensure less than a whole heart still spawns heart object
        maxHealth = Mathf.CeilToInt(maxHealth / 4.0f);
        
        int heartCount = _heartPanel.childCount;
    
        if(maxHealth > heartCount)
        {
            int heartsToAdd = maxHealth - heartCount;
    
            for(int i = 0; i < heartsToAdd; i++)
            {
                _heartPrefab = _heartUXML.CloneTree("Heart");
                _heartPanel.Add(_heartPrefab);
            }
            SetHealthBar(PlayerMaxHealth); //Since we added hearts, update heart graphics so the new hearts respect current health value.
        }
        else if(maxHealth < heartCount)
        {
            int heartsToRemove = heartCount - maxHealth;
            for(int i = 0; i < heartsToRemove; i++)
            {
                int lastHeartIndex = _heartPanel.childCount - 1;
                _heartPanel.Remove(_heartPanel.ElementAt(lastHeartIndex));
            }
        } 
    }// end update health bar count

}
