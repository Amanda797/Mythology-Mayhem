
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
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

    void Start()
    {
        heartState = GetComponentInChildren<PlayerHeartState>();
        if (!useShipHealth)
        {
            UpdateHealth();
        }
        else
        {
            UpdateShipHealthBarCount(ShipMaxHealth);
            SetShipHealthBar(ShipCurrHealth);
        }
    }

    public void UpdateHealth()
    {
        UpdateHealthBarCount(GameManager.instance.gameData.saveData.playerData.maxHealth);
        SetHealthBar(GameManager.instance.gameData.saveData.playerData.curHealth);
    }

    private void SetHealthBar(float health)
    {
        heartState.SetHealthBar(Mathf.CeilToInt(health));
    }

    public void UpdateHealthBarCount(float maxHealth)
    {
        heartState.UpdateHealthBarCount(Mathf.CeilToInt(maxHealth));
    }

    private void SetShipHealthBar(float health)
    {
        heartState.SetShipHealthBar(Mathf.CeilToInt(health));
    }

    public void UpdateShipHealthBarCount(float maxHealth)
    {
        heartState.UpdateShipHealthBarCount(Mathf.CeilToInt(maxHealth));
    }
}
