
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;

public class HealthUIController : MonoBehaviour
{
    //UI Components
    [Header("UI Components")]
    //private UIDocument _doc;
    //private VisualElement _heartPanel;
    //[SerializeField] private VisualTreeAsset _heartUXML;
    [SerializeField] private PlayerHeartState heartState;
    //private VisualElement _heartPrefab;
    [SerializeField] private List<Sprite> _heartModes;

    //private VisualElement _shipPanel;
    //[SerializeField] private VisualTreeAsset _shipUXML;
    //private VisualElement _shipPrefab;
    [SerializeField] private List<Sprite> _shipModes;
    public bool useShipHealth;


    //External Components
    [Header("External Components")]
    [SerializeField] public PlayerStats_SO ps;

    //Properties
    public float PlayerCurrHealth
    {
        get {
           return ps.CurrHealth; 
        }
        set {
            ps.CurrHealth = Mathf.Clamp(value, 0, PlayerMaxHealth);
            SetHealthBar(PlayerCurrHealth);
            UpdateHealthBarCount(PlayerMaxHealth);
        }
    }

    public float PlayerMaxHealth
    {
        get {
            return ps.MaxHealth;
        }
        set {
            ps.MaxHealth = Mathf.Max(0, value);
            UpdateHealthBarCount(PlayerMaxHealth);
        }
    }
    public float ShipCurrHealth
    {
        get
        {
            return ps.CurrShipHealth;
        }
        set
        {
            ps.CurrShipHealth = Mathf.Clamp(value, 0, ShipMaxHealth);
            SetShipHealthBar(ShipCurrHealth);
            UpdateShipHealthBarCount(ShipMaxHealth);
        }
    }

    public float ShipMaxHealth
    {
        get
        {
            return ps.MaxShipHealth;
        }
        set
        {
            ps.MaxShipHealth = Mathf.Max(0, value);
            UpdateShipHealthBarCount(ShipMaxHealth);
        }
    }

    /*void OnEnable() {
        _doc = GetComponent<UIDocument>();
        if (!useShipHealth)
        {
            _heartPanel = _doc.rootVisualElement.Q("PlayerHealth");
            //Works in Build with a Resource Folder in Assets
            _heartUXML = Resources.Load<VisualTreeAsset>("UI Toolkit/Heart-UIBP");
            _heartPrefab = _heartUXML.CloneTree("Heart");

            _heartPanel.Add(_heartPrefab);

        }
        else
        {
            _shipPanel = _doc.rootVisualElement.Q("PlayerHealth");

            _shipUXML = Resources.Load<VisualTreeAsset>("UI Toolkit/ShipHealth-UIBP");
            _shipPrefab = _shipUXML.CloneTree("Ship");
            _shipPanel.Add(_shipPrefab);
        }
    }*/

    void Start()
    {
        if (!useShipHealth)
        {
            UpdateHealthBarCount(PlayerMaxHealth);
            SetHealthBar(PlayerCurrHealth);
        }
        else
        {
            UpdateShipHealthBarCount(ShipMaxHealth);
            SetShipHealthBar(ShipCurrHealth);
        }
    }

    private void SetHealthBar(float health) {
        heartState.SetHealthBar(Mathf.CeilToInt(health));
        /*for (int i = 0; i < _heartPanel.childCount; i++)
        {
            float remainderHealth = Mathf.Clamp(health - (i * 4), 0, 4);
    
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
            }
        }*/
    }// end set health bar

    //Used to update the amount of Heart sprites in the Health Bar automatically.
    public void UpdateHealthBarCount(float maxHealth)
    {
        heartState.UpdateHealthBarCount(Mathf.CeilToInt(maxHealth));
        //Convert maxHealth "quarter hearts" value into whole hearts value.
        //Ceil to ensure less than a whole heart still spawns heart object
        /*maxHealth = Mathf.CeilToInt(maxHealth / 4.0f);
        
        int heartCount = _heartPanel.childCount;
    
        if(maxHealth > heartCount)
        {
            float heartsToAdd = maxHealth - heartCount;
    
            for(int i = 0; i < heartsToAdd; i++)
            {
                _heartPrefab = _heartUXML.CloneTree("Heart");
                _heartPanel.Add(_heartPrefab);
            }
            SetHealthBar(PlayerMaxHealth); //Since we added hearts, update heart graphics so the new hearts respect current health value.
        }
        else if(maxHealth < heartCount)
        {
            float heartsToRemove = heartCount - maxHealth;
            for(int i = 0; i < heartsToRemove; i++)
            {
                int lastHeartIndex = _heartPanel.childCount - 1;
                _heartPanel.Remove(_heartPanel.ElementAt(lastHeartIndex));
            }
        }*/
    }// end update health bar count

    private void SetShipHealthBar(float health)
    {
        heartState.SetShipHealthBar(Mathf.CeilToInt(health));
        /*for (int i = 0; i < _shipPanel.childCount; i++)
        {
            float remainderHealth = Mathf.Clamp(health - (i * 5), 0, 5);
            switch (remainderHealth)
            {
                case 0:
                    _shipPanel.ElementAt(i).Q<VisualElement>("Ship").style.backgroundImage = new StyleBackground(_shipModes[5]);
                    break;
                case 1:
                    _shipPanel.ElementAt(i).Q<VisualElement>("Ship").style.backgroundImage = new StyleBackground(_shipModes[4]);
                    break;
                case 2:
                    _shipPanel.ElementAt(i).Q<VisualElement>("Ship").style.backgroundImage = new StyleBackground(_shipModes[3]);
                    break;
                case 3:
                    _shipPanel.ElementAt(i).Q<VisualElement>("Ship").style.backgroundImage = new StyleBackground(_shipModes[2]);
                    break;
                case 4:
                    _shipPanel.ElementAt(i).Q<VisualElement>("Ship").style.backgroundImage = new StyleBackground(_shipModes[1]);
                    break;
                case 5:
                    _shipPanel.ElementAt(i).Q<VisualElement>("Ship").style.backgroundImage = new StyleBackground(_shipModes[0]);
                    break;
            }
        }*/
    }

    public void UpdateShipHealthBarCount(float maxHealth)
    {
        heartState.UpdateShipHealthBarCount(Mathf.CeilToInt(maxHealth));
        /*maxHealth = Mathf.CeilToInt(maxHealth / 5.0f);
        print(maxHealth);
        int shipCount = _shipPanel.childCount;

        if (maxHealth > shipCount)
        {
            float shipsToAdd = maxHealth - shipCount;

            for (int i = 0; i < shipsToAdd; i++)
            {
                _shipPrefab = _shipUXML.CloneTree("Ship");
                _shipPanel.Add(_shipPrefab);
            }
            SetShipHealthBar(ShipMaxHealth);
        }
        else if (maxHealth < shipCount)
        {
            float shipsToRemove = shipCount - maxHealth;
            for (int i = 0; i < shipsToRemove; i++)
            {
                int lastShipIndex = _shipPanel.childCount - 1;
                _shipPanel.Remove(_shipPanel.ElementAt(lastShipIndex));
            }
        }*/
    }

}
