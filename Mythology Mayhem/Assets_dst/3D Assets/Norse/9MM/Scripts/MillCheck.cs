using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MillCheck
{
    public int x1;
    public int y1;
    public int x2;
    public int y2;

    public MillCheck(int _x1, int _y1, int _x2, int _y2) 
    {
        x1 = _x1;
        y1 = _y1;
        x2 = _x2;
        y2 = _y2;
    }
}
