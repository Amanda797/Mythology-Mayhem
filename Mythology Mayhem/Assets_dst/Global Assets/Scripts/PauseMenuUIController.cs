using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuUIController : MonoBehaviour
{
    //UI Components
    [Header("UI Components")]
    private UIDocument _doc;


    //External Components
    [Header("External Components")]
    [SerializeField] public PlayerStats_SO ps;

    void OnEnable() {
        _doc = GetComponent<UIDocument>();
    }

}
