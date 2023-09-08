using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mill
{
    public NineMMMarbleScript marble1;
    public NineMMMarbleScript marble2;
    public NineMMMarbleScript marble3;

    public Mill(NineMMMarbleScript _marble1, NineMMMarbleScript _marble2, NineMMMarbleScript _marble3) 
    {
        marble1 = _marble1;
        marble2 = _marble2;
        marble3 = _marble3;
    }
}
