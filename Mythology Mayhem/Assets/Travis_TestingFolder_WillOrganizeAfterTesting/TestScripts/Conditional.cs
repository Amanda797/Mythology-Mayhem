using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Conditional
{
    public string conditionName;
    public Conditions.Condition type;

    public bool setToggle;
    public int addCount;
    public float addFloatCount;
    public string setWord;


    public Conditional(string _name, Conditions.Condition _type, bool _toggle, int _count, float _floatCount, string _word) 
    {
        conditionName = _name;
        type = _type;

        setToggle = _toggle;
        addCount = _count;
        addFloatCount = _floatCount;
        setWord = _word;
    }

    public void SetConditionValue(Conditions condition)
    {
        if (type == Conditions.Condition.Toggle)
        {
            condition.SetToggle(setToggle);
        }
        if (type == Conditions.Condition.Count)
        {
            condition.AddCount(addCount);
        }
        if (type == Conditions.Condition.FloatCount)
        {
            condition.AddFloatCount(addFloatCount);
        }
        if (type == Conditions.Condition.Word)
        {
            condition.SetWord(setWord);
        }
    }

    public void SetConditionValue(List<Conditions> conditionsList)
    {
        for (int i = 0; i < conditionsList.Count; i++)
        {
            if (conditionsList[i].conditionName == conditionName && conditionsList[i].condition == type)
            {
                if (type == Conditions.Condition.Toggle)
                {
                    conditionsList[i].SetToggle(setToggle);
                }
                if (type == Conditions.Condition.Count)
                {
                    conditionsList[i].AddCount(addCount);
                }
                if (type == Conditions.Condition.FloatCount)
                {
                    conditionsList[i].AddFloatCount(addFloatCount);
                }
                if (type == Conditions.Condition.Toggle)
                {
                    conditionsList[i].SetWord(setWord);
                }
            }
        }
        
    }
}
