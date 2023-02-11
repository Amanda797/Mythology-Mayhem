using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GraphicsDropdown : MonoBehaviour
{
    TMP_Dropdown dropdown;

    // Start is called before the first frame update
    public void DropdownHandler() {
        QualitySettings.SetQualityLevel(dropdown.value + 1);
    }
}
