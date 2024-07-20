
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
//using UnityEngine.UIElements;

public class HealthUIController : MonoBehaviour
{
    GameManager gameManager;
    //UI Components
    [Header("UI Components")]
    public PlayerHeartState heartState;
    [SerializeField] private List<Sprite> _heartModes;
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
        if (GameManager.instance != null) gameManager = GameManager.instance;
        else Debug.LogWarning("GameManager Missing.");

        if (!useShipHealth) UpdateHealth();
        else
        {
            UpdateShipHealthBarCount(ShipMaxHealth);
            SetShipHealthBar(ShipCurrHealth);
        }
    }

    public void UpdateHealth()
    {
        if (!heartState.gameObject.activeSelf) heartState.gameObject.SetActive(true);

        UpdateHealthBarCount(gameManager.gameData.maxHealth);
        SetHealthBar(gameManager.gameData.curHealth);
        gameManager.SaveGame();
    }

    private void SetHealthBar(float health)
    {
        heartState.SetHealthBar(Mathf.CeilToInt(health));
        ps.CurrHealth = health;
    }

    public void UpdateHealthBarCount(float maxHealth)
    {
        heartState.UpdateHealthBarCount(Mathf.CeilToInt(maxHealth));
        ps.MaxHealth = maxHealth;
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
