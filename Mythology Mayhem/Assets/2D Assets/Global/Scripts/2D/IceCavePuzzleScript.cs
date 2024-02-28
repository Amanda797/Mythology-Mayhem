using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCavePuzzleScript : MonoBehaviour
{
    public UniversalLeverScript[] levers1;
    public List<UniversalLeverScript.LeverPosition> pattern1;
    public bool Rune1;
    public PillarScript pillar1;
    public CaveDoorUniversalScript Door1;

    public UniversalLeverScript[] levers2;
    public List<UniversalLeverScript.LeverPosition> pattern2;
    public bool Rune2;
    public PillarScript pillar2;
    public CaveDoorUniversalScript Door2;

    public UniversalLeverScript[] levers3;
    public List<UniversalLeverScript.LeverPosition> pattern3;
    public bool Rune3;
    public PillarScript pillar3;
    public CaveDoorUniversalScript Door3;

    public PillarScript pillar4;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckLevers(levers1, pattern1))
        {
            if (pillar1.currentPosition != PillarScript.PillarPosition.Down && pillar1.canActivate)
            {
                pillar1.SwitchState();
            }
            Door1.canActivate = true;
        }
        else 
        {
            if (pillar1.currentPosition == PillarScript.PillarPosition.Down)
            {
                pillar1.SwitchState();
            }
            Door1.canActivate = false;
        }
        if (CheckLevers(levers2, pattern2))
        {
            if (pillar2.currentPosition != PillarScript.PillarPosition.Down && pillar2.canActivate)
            {
                pillar2.SwitchState();
            }
            Door2.canActivate = true;
        }
        else
        {
            if (pillar2.currentPosition == PillarScript.PillarPosition.Down)
            {
                pillar2.SwitchState();
            }
            Door2.canActivate = false;
        }
        if (CheckLevers(levers3, pattern3))
        {
            if (pillar3.currentPosition != PillarScript.PillarPosition.Down && pillar3.canActivate)
            {
                pillar3.SwitchState();
            }
            Door3.canActivate = true;
        }
        else
        {
            if (pillar3.currentPosition == PillarScript.PillarPosition.Down)
            {
                pillar3.SwitchState();
            }
            Door3.canActivate = false;
        }
    }

    bool CheckLevers(UniversalLeverScript[] leversToCheck, List<UniversalLeverScript.LeverPosition> pattern) 
    {
        bool correct = true;
        for (int i = 0; i < leversToCheck.Length; i++) 
        {
            if (leversToCheck[i].currentPosition != pattern[i]) 
            {
                correct = false;
            }
        }
        return correct;
    }

    public void PickupRune(int which) 
    {
        switch (which) 
        {
            case 1:
                Rune1 = true;
                break;
            case 2:
                Rune2 = true;
                break;
            case 3:
                Rune3 = true;
                break;
        }
    }

    public void CompletePuzzle() 
    {
        if (pillar4.currentPosition == PillarScript.PillarPosition.Up)
        {
            pillar4.SwitchState();
        }
    }
}
