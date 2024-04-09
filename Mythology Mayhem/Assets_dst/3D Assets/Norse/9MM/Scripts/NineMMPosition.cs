using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NineMMPosition
{
    public NineMMMarble marble;
    public int x;
    public int y;

    public enum NineMMMarble
    {
        FirstMarble,
        SecondMarble,
        None,
        Invalid
    }

    public NineMMPosition(NineMMMarble _marble, int _x, int _y)
    {
        marble = _marble;
        x = _x;
        y = _y;
    }
}
