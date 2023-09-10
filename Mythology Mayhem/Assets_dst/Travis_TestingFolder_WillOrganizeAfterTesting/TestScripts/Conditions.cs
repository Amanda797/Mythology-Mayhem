using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Conditions
{
    public string conditionName;
    public Condition condition;
    public bool toggle;
    public int count;
    public float floatCount;
    public string word;

    public bool currentToggle;
    public int currentCount;
    public float currentFloatCount;
    public string currentWord;

    public bool completed;

    public enum Condition 
    { 
        Toggle,
        Count,
        FloatCount,
        Word
    }


    public Conditions(Condition _condition) 
    {
        condition = _condition;
    }

    public void SetToggle(bool desired) 
    {
        currentToggle = desired;
    }

    public void AddCount(int desired) 
    {
        currentCount += desired;
    }

    public void AddFloatCount(float desired) 
    {
        currentFloatCount += desired;
    }

    public void SetWord(string desired) 
    {
        currentWord = desired;
    }
}

