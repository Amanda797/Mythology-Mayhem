using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionPoint : MythologyMayhem
{
    public LocalGameManager localGameManager;
    public Level sceneToTransition;
    public bool keyPress;
    public bool isActive;

    public List<Conditions> conditions;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckInput()
    {
        if (localGameManager.inScene == localGameManager.mainGameManager.currentScene && isActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                keyPress = true;
                StartCoroutine(ButtonPress());
            }
        }
    }

    IEnumerator ButtonPress()
    {
        yield return new WaitForSeconds(0.25f);
        keyPress = false;
        yield return null;
    }

    public bool CheckConditionsMeet() 
    {
        bool isComplete = true;
        for (int i = 0; i < conditions.Count; i++) 
        {
            if (conditions[i].condition == Conditions.Condition.Toggle) 
            {
                if (conditions[i].currentToggle == conditions[i].toggle)
                {
                    conditions[i].completed = true;
                }
                else 
                {
                    conditions[i].completed = false;
                    isComplete = false;
                }
            }
            if (conditions[i].condition == Conditions.Condition.Count)
            {
                if (conditions[i].currentCount == conditions[i].count)
                {
                    conditions[i].completed = true;
                }
                else
                {
                    conditions[i].completed = false;
                    isComplete = false;
                }
            }
            if (conditions[i].condition == Conditions.Condition.FloatCount)
            {
                if (conditions[i].currentFloatCount == conditions[i].floatCount)
                {
                    conditions[i].completed = true;
                }
                else
                {
                    conditions[i].completed = false;
                    isComplete = false;
                }
            }
            if (conditions[i].condition == Conditions.Condition.Word)
            {
                if (conditions[i].currentWord == conditions[i].word)
                {
                    conditions[i].completed = true;
                }
                else
                {
                    conditions[i].completed = false;
                    isComplete = false;
                }
            }
        }

        return isComplete;
    }
}
