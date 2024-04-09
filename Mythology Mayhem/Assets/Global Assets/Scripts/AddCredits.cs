using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddCredits : MonoBehaviour
{
    public GameObject groupPrefab;
    public GameObject textPrefab;

    public Credit[] credits;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Credit _credit in credits)
        {
            GameObject newCredit = Instantiate(groupPrefab, gameObject.transform);
            GameObject titleCredit = Instantiate(textPrefab, newCredit.transform);
            GameObject nameCredit = Instantiate(textPrefab, newCredit.transform);

            titleCredit.GetComponent<TextMeshProUGUI>().text = _credit._Title;
            titleCredit.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
            nameCredit.GetComponent<TextMeshProUGUI>().text = _credit._Name;
        }
    }
}

[System.Serializable]
public class Credit
{
    public string _Title;
    public string _Name;

    public Credit(string title, string name)
    {
        _Title = title;
        _Name = name;
    }
}
