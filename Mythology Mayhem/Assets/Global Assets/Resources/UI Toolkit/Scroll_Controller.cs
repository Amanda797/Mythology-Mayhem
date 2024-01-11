using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class Scroll_Controller : MonoBehaviour
{
    //UI Components
    [Header("UI Components")]
    private UIDocument _doc;
    private VisualElement _scrollPanel;
    private VisualElement _scrollText;
    private VisualTreeAsset _scrollUXML;
    private ScrollView _scrollView;
    private Label _text;

    private void OnEnable()
    {
        _doc = GetComponent<UIDocument>();

        _scrollPanel = _doc.rootVisualElement.Q("ScrollView");
        _scrollUXML = Resources.Load<VisualTreeAsset>("UI Toolkit/ScrollUI");
        _scrollPanel.Add(_scrollUXML.CloneTree());

        _scrollView = _scrollPanel.Q<ScrollView>("ScrollText");
        _scrollText = _doc.rootVisualElement.Q("ScrollText");

        _text = new Label();
        _text.style.unityFont = _doc.rootVisualElement.style.unityFont;
        _text.style.unityFontDefinition = _doc.rootVisualElement.style.unityFontDefinition;
        _text.style.whiteSpace = WhiteSpace.Normal;
        _text.text = "OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text OnEnable Scroll Text";

        _scrollText.Add(_text);

        CloseScroll();
    }

    public void OpenScroll(string NewText)
    {
        CloseScroll();

        _text.text = NewText;
        _scrollPanel.Add(_scrollUXML.CloneTree());
        _scrollPanel.Q<ScrollView>("ScrollText").Add(_text);
    }

    public void CloseScroll()
    {
        _text.text = "";
        _scrollPanel.Clear();
    }
}
