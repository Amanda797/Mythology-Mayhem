using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaflPosition
{
    public TaflPieces piece;
    public int x;
    public int y;

    public enum TaflPieces
    {
        King,
        AltKing,
        Pawn,
        AltPawn,
        None
    }

    public enum TaflDirection 
    { 
        North,
        South,
        East,
        West
    }

    public TaflPosition(TaflPieces _piece, int _x, int _y) 
    {
        piece = _piece;
        x = _x;
        y = _y;
    }
}
