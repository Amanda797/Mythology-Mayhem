using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Xml;

public class PauseMenuUIController : MonoBehaviour
{
    private UIDocument _pauseMenu;
    private VisualElement pauseMenuContainer;
    [SerializeField] Sprite pauseBackground;

    [SerializeField] private VisualTreeAsset _pauseScreen;
    [SerializeField] private VisualTreeAsset _optionsScreen;
    [SerializeField] private VisualTreeAsset _creditsScreen;
    [SerializeField] private VisualTreeAsset _helpScreen;

    [SerializeField] private VisualElement _pauseScreenPrefab;
    [SerializeField] private VisualElement _optionsScreenPrefab;
    [SerializeField] private VisualElement _creditsScreenPrefab;
    [SerializeField] private VisualElement _helpScreenPrefab;

    [SerializeField] string creditsPath;
    [SerializeField] string helpPath;

    private void OnEnable()
    {
        _pauseMenu = GetComponent<UIDocument>();
        pauseMenuContainer = _pauseMenu.rootVisualElement.Q("MenuContainer");
        _pauseMenu.rootVisualElement.Q("PauseMenuBackground").style.backgroundImage = new StyleBackground(pauseBackground);

        //Find Menu UXMLs
        _pauseScreen = Resources.Load<VisualTreeAsset>("UI Toolkit/Pause-UIBP");
        _optionsScreen = Resources.Load<VisualTreeAsset>("UI Toolkit/Options-UIBP");
        _creditsScreen = Resources.Load<VisualTreeAsset>("UI Toolkit/Credits-UIBP");
        _helpScreen = Resources.Load<VisualTreeAsset>("UI Toolkit/Help-UIBP");

        //Clone prefabs
        _pauseScreenPrefab = _pauseScreen.CloneTree();
        _optionsScreenPrefab = _optionsScreen.CloneTree();
        _creditsScreenPrefab = _creditsScreen.CloneTree();
        _helpScreenPrefab = _helpScreen.CloneTree();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Start with pause screen options
        pauseMenuContainer.Add(_pauseScreenPrefab);
    }

    
}
